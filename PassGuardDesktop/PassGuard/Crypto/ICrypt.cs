using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Interface that holds all the methods for each Encryption/Decryption algorithm...
	/// </summary>
	internal interface ICrypt
	{
		public void Encrypt(byte[] key, String src, String dst); //AES Encryption for files (bitsize defined by key)
		public void Decrypt(byte[] key, String src, String dst); //AES Decryption for files (bitsize defined by key)
		public String EncryptText(byte[] key, String src); //AES Encryption for text (bitsize defined by key)
		public String DecryptText(byte[] key, String src); //AES Decryption for text (bitsize defined by key)

	}
}
