using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text.Json;

namespace PassGuard.Core
{
    //Class with many not UI-related methods.
    class Utils
    {
        //Returns the SHA1 equivalent of a String password
        internal string ComputeSHA1(string password)
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
                    sb.Append(b.ToString("x2"));
                }
                string hash = sb.ToString();//Compose bytes to string

                return hash;
            }

        }

        //Obtain all the hashes that start with headHash from an API of pwned passwords.
        internal async Task<string> GetHashes(string headHash)
        {
            string instruction = "https://api.pwnedpasswords.com/range/";
            string url = instruction + headHash.ToUpper();
            HttpClient client = new();
            try
            {
                //HTTP request to the API and await for the list of hashes and their appearances in the API....
                string response = await client.GetStringAsync(url); 
                return response;
            }
            catch (HttpRequestException) //Error control
            {
                return "";
            }
            
        }

        //Compound the SHA1 of password, get all the hashes with a headhash, and if the compound is in the hashes returns true as password has been pwned, if not returns false
        internal async Task<bool> CheckPwnage(string password)
        {
            string hash = ComputeSHA1(password); //Compute SHA1
            string headhash = hash.Substring(0, 5); //Compute first part of hash in order to check hashes.
            string PwnedHashes = await GetHashes(headhash);

            StringReader reader = new(PwnedHashes);
            string line, tailHash;
            while ((line = reader.ReadLine()) != null)//Read all pwned hashes
            {
                tailHash = line.Substring(0, 35);
                if (headhash.ToUpper() + tailHash.ToUpper() == hash.ToUpper()) //If match, pass has been pwned before.
                {
                    return true;
                }
            }
            return false;
        }

        

        //Function to encrypt a src file into a dst file given a key with AES.
        internal void Encrypt(byte[] key, String src, String dst)
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

        //Function to derive a 256bit key (with PBKDF2) given a password and a salt.
        internal byte[] GetVaultKey(String password, byte[] salt)
        {
            Rfc2898DeriveBytes d1 = new(password, salt, iterations: 100100, HashAlgorithmName.SHA256);
            return d1.GetBytes(32); //256bit key.
        }

        //Function to decrypt a src file into a dst file given a key with AES.
        internal void Decrypt(byte[] key, String src, String dst)
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

        //Encrypt/Decrypt need key and salt.
        //EncryptText/DecryptText need key and IV.
            //If we encrypt the same text two times, diff ciphertext will be returned due to random IV.
            //Not sure what happens if we encrypt with same key and salt two times the same file.

        //Encrypt a src text with a key using AES, IV is prepended initially.
        internal String EncryptText(byte[] key, String src)
        {
            try
            {
                byte[] encrypted;
                byte[] IV;

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

        //Decrypts a src text with a key, separating IV from ciphertext.
        internal String DecryptText(byte[] key, String src)
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
