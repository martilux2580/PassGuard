using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Class that implements Interface IKDF as it holds functions for PBKDF2 Key Derivation Function....
	/// </summary>
	internal class PBKDF2Function : IKDF
	{
		/// <summary>
		/// Function to derive a 256bit key (with PBKDF2) given a password and a salt.
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public byte[] GetVaultKey(String password, byte[] salt, int bytes)
		{
			Rfc2898DeriveBytes d1; //Given bits, calculate PBKDF2 with corresponding hash algorithm and iterations
			switch (bytes*8)
			{
				case 256:
					d1 = new Rfc2898DeriveBytes(password, salt, 650060, HashAlgorithmName.SHA256);
					break;
				case 384:
					d1 = new Rfc2898DeriveBytes(password, salt, 650060, HashAlgorithmName.SHA384);
					break;
				case 512:
					d1 = new Rfc2898DeriveBytes(password, salt, 650060, HashAlgorithmName.SHA512);
					break;
				default:
					d1 = new Rfc2898DeriveBytes(password, salt, 650060, HashAlgorithmName.SHA256);
					break;
			}
			
			return d1.GetBytes(bytes); //Get key...
		}
	}
}
