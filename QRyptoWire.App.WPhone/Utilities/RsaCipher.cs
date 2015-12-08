using System.Security.Cryptography;

namespace QRyptoWire.App.WPhone.Utilities
{
    public class RsaCipher
    {
        public const int KeySize = 1024;
        private readonly RSACryptoServiceProvider _rsa;

        public RsaCipher()
        {
            _rsa = new RSACryptoServiceProvider(KeySize);
        }

        public RsaCipher(string key)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.FromXmlString(key);
        }

        public byte[] Encrypt(byte[] data)
        {
            return _rsa.Encrypt(data, false);
        }

        public byte[] Decrypt(byte[] data)
        {
            return _rsa.Decrypt(data, false);
        }

        public byte[] CreateDigitalSignature(byte[] data)
        {
           return  _rsa.SignData(data, new SHA1Managed());
        }

        public bool VerifyDigitalSignature(byte[] data, byte[] signature)
        {
            return _rsa.VerifyData(data, new SHA1Managed(), signature);
        }

        public static string GenerateKeyPair()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KeySize);
            return rsa.ToXmlString(true);
        }
    }
}
