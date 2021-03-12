using System;

namespace Opena3XX.Eventing.Msfs
{
    internal static class Program
    {
        private static FsConnect _fsConnect;

        public static void Main(string[] args)
        {
            try
            {
                _fsConnect = new FsConnect();

                try
                {
                    Console.WriteLine($"Connecting to Flight Simulator on 127.0.0.1:500");
                    _fsConnect.Connect("FsConnectTestConsole", "127.0.0.1" , 500, SimConnectProtocol.Ipv4);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                _fsConnect.SetText("OpenA3XX Sim Connector: Connected", 5);

                while (true)
                {
                    var calculatorCode = Console.ReadLine();
                    _fsConnect.SetEventId(calculatorCode);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e);
            }
        }
        
        
    }
}