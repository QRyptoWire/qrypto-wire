using System;

namespace QRyptoWire.Core.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
            : base(message)
        { }

        public UserNotFoundException()
            : base()
        { }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
