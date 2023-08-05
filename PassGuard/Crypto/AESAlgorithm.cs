using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Class that implements Interface ICrypt as it holds functions for AES Encryption/Decryption algorithm....
	///	
	/// As a note, if we encrypt the same text (files should do also) two times, diff ciphertext (or cipherfile) will be returned due to random IV.
	/// </summary>
	internal class AESAlgorithm : ICrypt
	{
		/// <summary>
		/// Function to encrypt a src file into a dst file given a key with AES.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		public void Encrypt(byte[] key, String src, String dst)
		{
			// Encrypt the source file and write it to the destination file.
			using (var sourceStream = File.OpenRead(src))
			using (var destinationStream = File.Create(dst))
			using (var provider = Aes.Create())
			{
				if (key != null)
				{
					provider.Key = key; //Set key
				}

				using (var cryptoTransform = provider.CreateEncryptor())
				using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
				{
					destinationStream.Write(provider.IV, 0, provider.IV.Length); //Writes a block of bytes from offset 0 to the length.
					sourceStream.CopyTo(cryptoStream);
				}
			}
		}

		/// <summary>
		/// Function to decrypt a src file into a dst file given a key with AES.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		public void Decrypt(byte[] key, String src, String dst)
		{
			// Decrypt the source file and write it to the destination file.
			using (var sourceStream = File.OpenRead(src))
			using (var destinationStream = File.Create(dst))
			using (var provider = Aes.Create())
			{
				var IV = new byte[provider.IV.Length];
				sourceStream.Read(IV, 0, IV.Length);
				using (var cryptoTransform = provider.CreateDecryptor(key, IV))
				using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
				{
					cryptoStream.CopyTo(destinationStream);
				}
			}
		}

		

		/// <summary>
		/// Encrypt a src text with a key using AES, IV is prepended initially otherwise would be difficult to decrypt.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="src"></param>
		/// <returns></returns>
		public String EncryptText(byte[] key, String src)
		{
			try
			{
				byte[] encrypted;
				byte[] IV;

				//Encrypt text
				using (var provider = Aes.Create())
				{
					provider.Key = key;

					provider.GenerateIV();
					IV = provider.IV;

					provider.Mode = CipherMode.CBC;

					//Create the streams used for encryption. 
					using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
					using (var msEncrypt = new MemoryStream())
					{
						using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(src);
						}
						encrypted = msEncrypt.ToArray();
					}

				}

				//Join the byte arrays of the IV prepended to the byte array of the ciphertext
				var combinedIvCt = new byte[IV.Length + encrypted.Length];
				Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
				Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

				//Return the encrypted bytes from the memory stream. 
				return Convert.ToBase64String(combinedIvCt);
			}
			catch (Exception)
			{
				return "void";
			}
		}

		/// <summary>
		/// Decrypts a src text with a key, separating IV from ciphertext.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="src"></param>
		/// <returns></returns>
		public String DecryptText(byte[] key, String src)
		{
			try
			{
				//Declare the string used to hold the decrypted text. 
				string plaintext = null;
				byte[] cipherTextCombined = Convert.FromBase64String(src);

				//Create an Aes object with the specified key and IV. 
				using (var provider = Aes.Create())
				{
					provider.Key = key;

					byte[] IV = new byte[provider.BlockSize / 8];
					byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

					//Get the IV that is prepended from the ciphertext...
					Array.Copy(cipherTextCombined, IV, IV.Length);
					Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

					provider.IV = IV;

					provider.Mode = CipherMode.CBC;

					//Create a decrytor to perform the stream transform.
					ICryptoTransform decryptor = provider.CreateDecryptor(provider.Key, provider.IV);

					//Create the streams used for decryption. 
					using (var msDecrypt = new MemoryStream(cipherText))
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					using (var srDecrypt = new StreamReader(csDecrypt))
					{
						//Read the decrypted bytes from the decrypting stream and place them in a string.
						plaintext = srDecrypt.ReadToEnd();
					}
				}
				return plaintext;
			}
			catch (Exception) //If error, return decrypted string.
			{
				return src;
			}

		}
	}
}
