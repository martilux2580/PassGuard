using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
    internal interface IKDF
    {
        public byte[] GetVaultKey(String password, byte[] salt, int bytes);
    }
}
