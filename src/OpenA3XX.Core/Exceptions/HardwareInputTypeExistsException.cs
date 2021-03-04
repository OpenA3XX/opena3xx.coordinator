using System;

namespace OpenA3XX.Core.Exceptions
{
    [Serializable]
    public class HardwareInputTypeExistsException : Exception
    {
        public HardwareInputTypeExistsException()
        {
        }

        public HardwareInputTypeExistsException(string message) : base(message)
        {
        }
    }
}