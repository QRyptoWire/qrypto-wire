using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRyptoWire.Core
{
    public class Contact
    {
        public Contact(int uuid, string name, string publicKey)
        {
            UUID = uuid;
            Name = name;
            PublicKey = publicKey;
        }

        public int UUID { get; private set; }
        public string Name { get; private set; }
        public string PublicKey { get; private set; }
    }
}