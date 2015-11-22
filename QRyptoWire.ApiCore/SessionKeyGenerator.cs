using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QRyptoWire.Service.Core
{
	public class SessionKeyGenerator
	{
		private const int KeyLength = 20;

		public static string GetUniqueKey()
		{

			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[KeyLength];

			rng.GetBytes(buff);
			return BytesToHexString(buff);
		}
		private static string BytesToHexString(IEnumerable<byte> bytes)
		{
			var hexString = new StringBuilder(64);

			foreach (var t in bytes)
			{
				hexString.Append($"{t:X2}");
			}
			return hexString.ToString();
		}
	}
}
