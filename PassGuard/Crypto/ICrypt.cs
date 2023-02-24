using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	internal interface ICrypt
	{
		public void Encrypt(byte[] key, String src, String dst);
		public void Decrypt(byte[] key, String src, String dst);
		public String EncryptText(byte[] key, String src);
		public String DecryptText(byte[] key, String src);
		
	}
}
