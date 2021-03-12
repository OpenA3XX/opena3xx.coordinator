namespace Opena3XX.Eventing.Msfs
{
    public enum SimConnectFileLocation
    {
        /// <summary>
        /// The SimConnect.cfg file will be written to the MyDocuments location.
        /// </summary>
        MyDocuments,
        /// <summary>
        /// The SimConnect.cfg file will be written to the same location that the application was started from.
        /// </summary>
        /// <remarks>
        /// The application must have write access to this folder.
        /// </remarks>
        Local
    }
}