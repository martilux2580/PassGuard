using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Interface that holds all the methods for each Hash Fucntion classes implemented
	/// </summary>
	internal interface IHash
	{
		public string Compute(string password, int bits); //SHA1 Hash function
	}
}
