using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;


namespace QRyptoWire.ApiCore.Services
{
	public class CryptoService
	{
		private const int SessionKeyLength = 20;
		private const int SaltLength = 8;

		public static string CreateSessionKey()
		{
			return GenerateRandomString(SessionKeyLength);
		}

		public static string CreateSalt()
		{
			return GenerateRandomString(SaltLength);
		}

		public static string CreatePasswordHash(string password)
		{
			var salt = CreateSalt();
            return salt + GetHash(password, salt);
		}

		private static string GetHash(string s, string salt)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(salt + s);
			var sha256 = new SHA256CryptoServiceProvider();
			return BytesToHexString(sha256.ComputeHash(bytes));
		}

		public static bool ValidatePassword(string hash, string password)
		{
			var saltValue = hash.Substring(0, SaltLength * 2);
			var hashedPassword = hash.Substring(SaltLength * 2);
			var checkedPassword = GetHash(password, saltValue);

			if (checkedPassword.Equals(hashedPassword, StringComparison.Ordinal))
			{
				return true;
			}
			return false;
		}

		public static string GenerateRandomString(int keyLength)
		{
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[keyLength];

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
