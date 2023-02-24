using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	internal interface IHash
	{
		public string Compute(string password, int bits);
	}
}
