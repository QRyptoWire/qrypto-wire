using System;
using QRyptoWire.Core.Objects;

namespace QRyptoWire.Core.Services.Stubs
{
    class EncryptionServiceStub : IEncryptionService
    {
        public bool Decrypt(EncryptedMessage encryptedMessage, int senderId, out string messageText)
        {
            throw new NotImplementedException();
        }

        EncryptedMessage IEncryptionService.Encrypt(string messageText, int recieverId)
        {
            throw new NotImplementedException();
        }

        public bool ComposePublicKey(string modulus, string exponent, out string publicKey)
        {
            publicKey = String.Empty;
            return true;
        }

        public Tuple<string, string> DecomposePublicKey(string publicKey)
        {
            return new Tuple<string, string>("qwertyqwertyqwertyqwertyqwertyqwertyqwertyqwerty", "ABCD");
        }

        public string ExtractPublicKey(string keyPair)
        {
            return string.Empty;
        }

        public string GetKeyPair()
        {
            return "wertyqwertyqwertyqwertyqwertyqwertyqwertyqwerty";
        }
    }
}
