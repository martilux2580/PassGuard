using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	internal static class CryptoUtils
	{
		//https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string
		//https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string/32932789#32932789
		internal static string GenerateSecurePassword(int length, string validCharacters) //StackOverflow
		{
			StringBuilder res = new();

			using (var cryptoProvider = RandomNumberGenerator.Create())
			{
				while (res.Length != length) //Compound passwords byte-a-byte
				{
					byte[] oneByte = new byte[1];
					cryptoProvider.GetBytes(oneByte);
					char character = (char)oneByte[0];
					if (validCharacters.Contains(character))
					{
						res.Append(character);
					}
				}
			}

			return res.ToString();
		}
	}
}
