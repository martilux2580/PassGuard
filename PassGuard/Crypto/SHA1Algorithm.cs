using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Class that implements Interface IHash as it holds functions for SHA1 hash algorithm....
	/// </summary>
	internal class SHA1Algorithm : IHash
	{
		/// <summary>
		/// Returns the SHA1 equivalent of a String password
		/// </summary>
		/// <param name="password"></param>
		/// <param name="bits"></param>
		/// <returns></returns>
		public string Compute(string password, int bits = 160)
		{
			if (String.IsNullOrEmpty(password)) //This if for controlling errors...
			{
				return null;
			}
			else
			{
				byte[] passByte = Encoding.UTF8.GetBytes(password);//Encode Pass
				byte[] passHash = SHA1.HashData(passByte);//Compute SHA1

				//Convert Hash into readable string
				StringBuilder sb = new();
				foreach (byte b in passHash)
				{
					sb.Append(b.ToString("x2")); //Format strings with 2 characters even if it only contains one....
				}
				string hash = sb.ToString();//Compose bytes to string

				return hash;
			}

		}
	}
}
