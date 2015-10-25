using System.Security.Cryptography;
using System.Text;

namespace QRyptoWire.Service.Data
{
	public class SessionKeyGenerator
	{
		private static readonly int keyLength = 20;

        public static string GetUniqueKey()
		{

			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[keyLength];

			rng.GetBytes(buff);
			return BytesToHexString(buff);
		}
		private static string BytesToHexString(byte[] bytes)
		{
			var hexString = new StringBuilder(64);

			foreach (byte t in bytes)
			{
				hexString.Append($"{t:X2}");
			}
			return hexString.ToString();
		}
	}
}
