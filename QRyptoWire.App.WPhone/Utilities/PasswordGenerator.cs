using System;
using System.Security.Cryptography;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class PasswordGenerator
    {
        public static string Next(int passwordLength)
        {
            if (passwordLength % 4 != 0)
            {
                throw new ArgumentException("Password length must be a multiple of 4");
            }

            var rngProvider = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[passwordLength / 4 * 3];
            rngProvider.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
