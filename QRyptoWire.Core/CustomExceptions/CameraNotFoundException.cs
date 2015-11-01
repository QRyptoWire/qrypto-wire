using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable.Serializers;

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
