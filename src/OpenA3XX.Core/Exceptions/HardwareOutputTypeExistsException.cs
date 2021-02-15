using System;

namespace OpenA3XX.Core.Exceptions
{
    [Serializable]
    public class HardwareOutputTypeExistsException: Exception
    {
        public HardwareOutputTypeExistsException()
        {
            
        }
        
        public HardwareOutputTypeExistsException(string message) : base(message)
        {
        }
    }
}