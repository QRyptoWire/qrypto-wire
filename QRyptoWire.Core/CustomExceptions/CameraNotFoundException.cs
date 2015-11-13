using System;

namespace QRyptoWire.Core.CustomExceptions
{
    public class CameraNotFoundException : Exception
    {
        public CameraNotFoundException(string message) 
            : base(message) { }

        public CameraNotFoundException() 
            : base() { }

        public CameraNotFoundException(string message, Exception inner)
            : base(message, inner) {  }
    }
}
