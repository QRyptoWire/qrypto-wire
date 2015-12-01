using System;

namespace QRyptoWire.Core.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string message, int recieverUUID);
        string Decrypt(string message, int senderUUID);
        bool ComposePublicKey(string modulus, string exponent, out string publicKey);
        Tuple<string, string> DecomposePublicKey(string publicKey);
        string ExtractPublicKey(string keyPair);
        string GetKeyPair();
    }
}