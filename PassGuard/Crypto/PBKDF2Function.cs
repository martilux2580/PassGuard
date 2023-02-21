using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
    internal class PBKDF2Function : IKDF
    {
        //Function to derive a 256bit key (with PBKDF2) given a password and a salt.
        public byte[] GetVaultKey(String password, byte[] salt, int bytes)
        {
            Rfc2898DeriveBytes d1;
            switch (bytes*8)
            {
                case 256:
                    d1 = new Rfc2898DeriveBytes(password, salt, 100100, HashAlgorithmName.SHA256);
                    break;
                case 384:
                    d1 = new Rfc2898DeriveBytes(password, salt, 100100, HashAlgorithmName.SHA384);
                    break;
                case 512:
                    d1 = new Rfc2898DeriveBytes(password, salt, 100100, HashAlgorithmName.SHA512);
                    break;
                default:
                    d1 = new Rfc2898DeriveBytes(password, salt, 100100, HashAlgorithmName.SHA256);
                    break;
            }
            
            return d1.GetBytes(bytes); //256bit key.
        }
    }
}
