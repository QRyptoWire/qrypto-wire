using System.IO;
using System.Security.Cryptography;

namespace QRyptoWire.App.WPhone.Utilities
{
    public class AesCipher
    {
        private const int KeySize = 256;
        private readonly AesManaged _aes;

        public byte[] Key => _aes.Key;
        public byte[] IV => _aes.IV;

        public AesCipher()
        {
            _aes = new AesManaged {KeySize = KeySize};
        }

        public AesCipher(byte[] key, byte[] iv)
        {
            _aes = new AesManaged {Key = key, IV = iv};
        }

        public byte[] Encrypt(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, _aes.CreateEncryptor(_aes.Key, _aes.IV), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, _aes.CreateDecryptor(_aes.Key, _aes.IV), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
