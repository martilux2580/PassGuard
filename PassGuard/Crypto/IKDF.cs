﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Crypto
{
	/// <summary>
	/// Interface that holds all the methods for each KDF key derivation function classes implemented
	/// </summary>
	internal interface IKDF
	{
		public byte[] GetVaultKey(String password, byte[] salt, int bytes); //PBKDF2 Key Derivation Function
	}
}
