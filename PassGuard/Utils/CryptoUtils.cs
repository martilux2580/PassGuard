using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	/// <summary>
	/// Static Utils class for methods that involve Cryptographic functions handling.
	/// </summary>
	internal static class CryptoUtils
	{
		/// <summary>
		/// Given a length and a string containing all valid characters, generates a random secure password containing only valid characters...
		/// Random passwords need to be generated securely, otherwise crackers could discover the seed and find out some malicious tricks :)
		/// Doc:
		/// https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string
		/// https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string/32932789#32932789
		/// </summary>
		/// <param name="length"></param>
		/// <param name="validCharacters"></param>
		/// <returns></returns>
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
