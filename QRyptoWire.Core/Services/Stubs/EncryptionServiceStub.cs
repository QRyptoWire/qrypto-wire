using System;

namespace QRyptoWire.Core.Services.Stubs
{
    class EncryptionServiceStub : IEncryptionService
    {
        public bool IsLazyMotherfucker()
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string message, int recieverUUID)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string message, int senderUUID)
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
            return new Tuple<string, string>(string.Empty, string.Empty);
        }

        public string ExtractPublicKey(string keyPair)
        {
            return string.Empty;
        }
    }
}
