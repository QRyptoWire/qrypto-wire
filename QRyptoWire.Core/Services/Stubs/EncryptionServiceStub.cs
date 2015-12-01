using System;

namespace QRyptoWire.Core.Services.Stubs
{
    class EncryptionServiceStub : IEncryptionService
    {
        public string Encrypt(string message, int recieverUUID)
        {
            return string.Empty;
        }

        public string Decrypt(string message, int senderUUID)
        {
            return string.Empty;
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
