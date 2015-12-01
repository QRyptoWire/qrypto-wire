using System.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace QRyptoWire.App.WPhone.Utilities
{
    public class RsaCipher
    {
        private const int KeySize = 384;
        private readonly RSACryptoServiceProvider _rsa;

        public string KeyPair => _rsa.ToXmlString(true);

        public RsaCipher()
        {
            _rsa = new RSACryptoServiceProvider(KeySize);
        }

        public RsaCipher(string key)
        {
            _rsa = new RSACryptoServiceProvider(KeySize);
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
    }
}
