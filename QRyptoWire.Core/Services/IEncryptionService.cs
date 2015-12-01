using System;
using QRyptoWire.Core.Objects;

namespace QRyptoWire.Core.Services
{
    public interface IEncryptionService
    {
        EncryptedMessage Encrypt(string messageText, int recieverId);
        bool Decrypt(EncryptedMessage encryptedMessage, int senderId, out string messageText);
        bool ComposePublicKey(string modulus, string exponent, out string publicKey);
        Tuple<string, string> DecomposePublicKey(string publicKey);
        string ExtractPublicKey(string keyPair);
        string GetKeyPair();
    }
}