using System;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class FormatConverter
    {
        public static byte[] StringToBytes(string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        public static string BytesToString(byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public static byte[] String64ToBytes(string data)
        {
            return Convert.FromBase64String(data);
        }

        public static string BytesToString64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
