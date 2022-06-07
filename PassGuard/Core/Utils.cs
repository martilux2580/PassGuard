using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Core
{
    class Utils
    {
        internal string ComputeSHA1(string password)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] passByte = Encoding.UTF8.GetBytes(password);//Encode Pass
            byte[] passHash = sha1.ComputeHash(passByte);//Compute SHA1

            //Convert Hash into readable string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in passHash)
            {
                sb.Append(b.ToString("x2"));
            }
            string hash = sb.ToString();//Compose bytes to string

            return hash;
        }

        //Obtain all the hashes that start with headHash from an API of pwned passwords.
        internal async Task<string> getHashes(string headHash)
        {
            string instruction = "https://api.pwnedpasswords.com/range/";
            string url = instruction + headHash.ToUpper();
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            return response;
        }

        internal async Task<bool> CheckPwnage(string password)//Password pwned before? -> returns true, Password not pwned before? -> returns false
        {
            string hash = ComputeSHA1(password); //Compute SHA1
            string headhash = hash.Substring(0, 5); //Compute first part of hash in order to check hashes.
            string PwnedHashes = await getHashes(headhash);

            StringReader reader = new StringReader(PwnedHashes);
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

        //https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string
        //https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string/32932789#32932789
        internal string GenerateSecurePassword(int length, string validCharacters) //StackOverflow xD
        {
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                while (res.Length != length)
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

        //Function to check if a string has Lower, Upper, Number or Symbol characters, based on those modes.
        internal bool Check(String str, String mode)
        {
            switch (mode)
            {
                case "Lower":
                    foreach (char c in str)
                    {
                        if (char.IsLower(c)) { return true; }
                    }
                    return false;
                case "Upper":
                    foreach (char c in str)
                    {
                        if (char.IsUpper(c)) { return true; }
                    }
                    return false;
                case "Number":
                    foreach (char c in str)
                    {
                        if (char.IsDigit(c)) { return true; }
                    }
                    return false;
                case "Symbol":
                    String validSymbols = "!@#~$%&¬€/()=?¿+*[]{}ñÑ-_.:,;<>¡ªº";
                    foreach (char c in str)
                    {
                        if (validSymbols.Contains(c)) { return true; }
                    }
                    return false;
                default:
                    return false;
            }
        }

        //Function to encrypt a src file into a dst file given a key with AES.
        internal void Encrypt(byte[] key, String src, String dst)
        {
            // Encrypt the source file and write it to the destination file.
            using (var sourceStream = File.OpenRead(src))
            using (var destinationStream = File.Create(dst))
            using (var provider = new AesCryptoServiceProvider())
            {
                if (key != null)
                {
                    provider.Key = key;
                }

                using (var cryptoTransform = provider.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    destinationStream.Write(provider.IV, 0, provider.IV.Length);
                    sourceStream.CopyTo(cryptoStream);
                }
            }
        }

        //Function to derive a 256bit key (with PBKDF2) given a password and a salt.
        internal byte[] getVaultKey(String password, byte[] salt)
        {
            Rfc2898DeriveBytes d1 = new Rfc2898DeriveBytes(password, salt, iterations: 100100);
            return d1.GetBytes(32); //256bit key.

        }

        //Function to decrypt a src file into a dst file given a key with AES.
        internal void Decrypt(byte[] key, String src, String dst)
        {
            // Decrypt the source file and write it to the destination file.
            using (var sourceStream = File.OpenRead(src))
            using (var destinationStream = File.Create(dst))
            using (var provider = new AesCryptoServiceProvider())
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

    }
}
