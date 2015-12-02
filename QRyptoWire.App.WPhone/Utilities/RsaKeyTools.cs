using System;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class RsaKeyTools
    {
        public static bool ComposePublicKey(string modulus, string exponent, out string publicKey)
        {
            XDocument doc = new XDocument(
                new XElement("RSAKeyValue",
                    new XElement("Modulus", modulus),
                    new XElement("Exponent", exponent)
                    )
                );

            publicKey = doc.ToString();
            return VerifyPublicKey(publicKey);
        }

        public static Tuple<string, string> DecomposePublicKey(string publicKey)
        {
            if (publicKey == null)
                throw new ArgumentException("Public key cannot be null");

            XDocument doc = XDocument.Parse(publicKey);
            string modulus = doc.Elements("RSAKeyValue").Single().Element("Modulus")?.Value;
            string exponent = doc.Elements("RSAKeyValue").Single().Element("Exponent")?.Value;

            if (modulus == null || exponent == null)
                throw new ArgumentException("Modulus and exponent values must be set");

            return new Tuple<string, string>(modulus, exponent);
        }

        public static string ExtractPublicKey(string keyPair)
        {
            if (keyPair == null)
                throw new ArgumentException("Key pair cannot be null");

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(keyPair);
            return rsaProvider.ToXmlString(false);
        }

        private static bool VerifyPublicKey(string publicKey)
        {
            try
            {
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                rsaProvider.FromXmlString(publicKey);
                return rsaProvider.KeySize == RsaCipher.KeySize;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }
    }
}
