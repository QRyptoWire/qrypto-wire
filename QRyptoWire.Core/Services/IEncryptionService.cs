using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRyptoWire.Core.Services
{
    public interface IEncryptionService
    {
        bool IsLazyMotherfucker();
        string Encrypt(string message, int recieverUUID);
        string Decrypt(string message, int senderUUID);
    }
}