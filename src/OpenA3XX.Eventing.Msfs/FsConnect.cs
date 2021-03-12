using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.FlightSimulator.SimConnect;

namespace Opena3XX.Eventing.Msfs
{
    public sealed class FsConnect : IFsConnect
    {
        private SimConnect _simConnect;

        private readonly EventWaitHandle _simConnectEventHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private Thread _simConnectReceiveThread;
        private bool _connected;

        #region Simconnect structures

        private enum Definitions
        {
            Struct1,
        }

        #endregion

        /// <inheritdoc />
        public bool Connected
        {
            get => _connected;
            private set
            {
                if (_connected == value) return;
                _connected = value;
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc />
        public SimConnectFileLocation SimConnectFileLocation { get; set; } = SimConnectFileLocation.MyDocuments;

        /// <inheritdoc />
        public event EventHandler ConnectionChanged;

        /// <inheritdoc />
        public event EventHandler<FsDataReceivedEventArgs> FsDataReceived;

        /// <inheritdoc />
        public event EventHandler<FsErrorEventArgs> FsError;

        /// <inheritdoc />
        public void Connect(string applicationName, uint configIndex = 0)
        {
            try
            {
                _simConnect = new SimConnect(applicationName, IntPtr.Zero, 0, _simConnectEventHandle, configIndex);
                LoadEventPresets();
            }
            catch (Exception e)
            {
                _simConnect = null;
                throw new Exception("Could not connect to Flight Simulator: " + e.Message, e);
            }

            _simConnectReceiveThread =
                new Thread(SimConnect_MessageReceiveThreadHandler) {IsBackground = true};
            _simConnectReceiveThread.Start();

            _simConnect.OnRecvOpen += SimConnect_OnRecvOpen;
            _simConnect.OnRecvQuit += SimConnect_OnRecvQuit;

            _simConnect.OnRecvException += SimConnect_OnRecvException;
            _simConnect.OnRecvSimobjectDataBytype += SimConnect_OnRecvSimobjectDataBytype;
            
        }

        /// <inheritdoc />
        public void Connect(string applicationName, string hostName, uint port, SimConnectProtocol protocol)
        {
            if (applicationName == null) throw new ArgumentNullException(nameof(applicationName));

            CreateSimConnectConfigFile(hostName, port, protocol);

            Connect(applicationName);
        }

        /// <inheritdoc />
        public void Disconnect()
        {
            if (!Connected) return;

            try
            {
                _simConnectReceiveThread.Abort();
                _simConnectReceiveThread.Join();

                _simConnect.OnRecvOpen -= SimConnect_OnRecvOpen;
                _simConnect.OnRecvQuit -= SimConnect_OnRecvQuit;
                _simConnect.OnRecvException -= SimConnect_OnRecvException;
                _simConnect.OnRecvSimobjectDataBytype -= SimConnect_OnRecvSimobjectDataBytype;
                
                _simConnect.Dispose();
            }
            catch
            {
                // ignored
            }
            finally
            {
                _simConnectReceiveThread = null;
                _simConnect = null;
                Connected = false;
            }
        }

        /// <inheritdoc />
        public void RegisterDataDefinition<T>(Enum id, List<SimProperty> definition) where T : struct
        {
            foreach (var item in definition)
            {
                _simConnect.AddToDataDefinition(id, item.Name, item.Unit, item.DataType, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            }

            _simConnect.RegisterDataDefineStruct<T>(id);
        }

        /// <inheritdoc />
        public void RequestData(Enum requestId)
        {
            _simConnect?.RequestDataOnSimObjectType( requestId, Definitions.Struct1, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }

        /// <inheritdoc />
        public void UpdateData<T>(Enum id, T data)
        {
            _simConnect?.SetDataOnSimObject(id, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        public void SetText(string text, int duration)
        {
            _simConnect.Text(SIMCONNECT_TEXT_TYPE.PRINT_BLACK, duration, Definitions.Struct1, text);
        }

        private enum SimConnectNotificationGroupId
        {
            SimConnectGroupPriorityDefault,
            SimConnectGroupPriorityHighest
        }
        public void SetEventId(string eventId)
        {
            
            if (_simConnect == null || !IsConnected()) return;

            
            Tuple<string, uint> eventItem = null;

            foreach (var groupKey in Events.Keys)
            {
                Console.WriteLine(groupKey + " --- " + Events[groupKey]);
                eventItem = Events[groupKey].Find(x => x.Item1 == eventId);
                if (eventItem != null) break;
            }

            if (eventItem == null)
            {
                Console.Write("SimConnectCache::setEventID: Unknown event ID: " + eventId);
                return;
            }
            
            
            _simConnect.TransmitClientEvent(
                0,
                (Opena3XXEvents)eventItem.Item2,
                1,
                SimConnectNotificationGroupId.SimConnectGroupPriorityDefault,
                SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
            );
        }


        private string _presetFile;

        private Dictionary<string, List<Tuple<string, uint>>> Events { get; set; }
        private void LoadEventPresets()
        {
            if (Events == null) Events = new Dictionary<string, List<Tuple<String, uint>>> ();
            Events.Clear();

            if (_presetFile == null) _presetFile = @"Events.txt";
            var lines = System.IO.File.ReadAllLines(_presetFile);
            var groupKey = "Dummy";
            uint eventIdx = 0;

            Events[groupKey] = new List<Tuple<string, uint>>();
            foreach (var line in lines)
            {
                if (line.StartsWith("//")) continue;

                var cols = line.Split(':');
                if (cols.Length > 1)
                {
                    groupKey = cols[0];
                    Events[groupKey] = new List<Tuple<string, uint>>();
                    continue; // we found a group
                }

                Events[groupKey].Add(new Tuple<string, uint>(cols[0], eventIdx++));
            }
        }

        private enum Opena3XXEvents
        {
            Dummy
        };
        
        private const string StandardEventGroup = "STANDARD";

        private bool IsConnected()
        {
            return _connected;
            //return true;
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            _connected = true;

            // register Events
            foreach (var groupKey in Events.Keys)
            {
                foreach (var (item1, item2) in Events[groupKey])
                {
                    var prefix = "";
                    if (groupKey != StandardEventGroup) prefix = "OpenA3XX.";
                    (sender).MapClientEventToSimEvent((Opena3XXEvents) item2, prefix + item1);
                }
            }
        }


        /// The case where the user closes game
        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Disconnect();
        }

        private static void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            FsDataReceived?.Invoke(this, new FsDataReceivedEventArgs()
            {
                RequestId = data.dwRequestID,
                Data = data.dwData[0]
            });
        }

        private void SimConnect_MessageReceiveThreadHandler()
        {
            while (true)
            {
                _simConnectEventHandle.WaitOne();

                try
                {
                    _simConnect?.ReceiveMessage();
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }

        private void CreateSimConnectConfigFile(string hostName, uint port, SimConnectProtocol protocol)
        {
            try
            {
                var sb = new StringBuilder();

                var protocolString = "Ipv4";

                switch (protocol)
                {
                    case SimConnectProtocol.Pipe:
                        protocolString = "Pipe";
                        break;
                    case SimConnectProtocol.Ipv4:
                        protocolString = "Ipv4";
                        break;
                    case SimConnectProtocol.Ipv6:
                        protocolString = "Ipv6";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(protocol), protocol, null);
                }

                sb.AppendLine("[SimConnect]");
                sb.AppendLine("Protocol=" + protocolString);
                sb.AppendLine($"Address={hostName}");
                sb.AppendLine($"Port={port}");

                var directory = "";
                directory = SimConnectFileLocation == SimConnectFileLocation.Local ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                var fileName = Path.Combine(directory ?? string.Empty, "SimConnect.cfg");

                File.WriteAllText(fileName, sb.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Could not create SimConnect.cfg file: " + e.Message, e);
            }
        }

        // To detect redundant calls
        private bool _disposed;

        /// <summary>
        /// Disconnects and disposes the client.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _simConnect?.Dispose();
            }

            _disposed = true;
        }

    }
}