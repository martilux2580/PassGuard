using PassGuard.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Pwned
{
	/// <summary>
	/// Static class that handles functions for checking, calculating or interacting with Pwned passwords.
	/// </summary>
	internal static class Pwned
	{
		/// <summary>
		/// Makes an HTTP Request (curl) with the Hash Header so that the API returns us all the pwned passwords that have as SHA1 header hash that hash header.
		/// 
		/// Obtain all the hashes that start with headHash from an API of pwned passwords.
		/// </summary>
		/// <param name="headHash"></param>
		/// <returns></returns>
		internal static async Task<string> GetHashes(string headHash)
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
				return "-1";
			}

		}

		//Compound the SHA1 of password, get all the hashes with a headhash, and if the compound is in the hashes returns true as password has been pwned, if not returns false
		/// <summary>
		/// Checks if password has been pwned by computing the SHA1 of the given pass and checking if in the API that hash has been pwned in a data breach...
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		internal static async Task<bool> CheckPwnage(string password)
		{
			IHash sha1 = new SHA1Algorithm();

			string hash = sha1.Compute(password, 160); //Compute SHA1
			string headhash = hash.Substring(0, 5); //Compute first part of hash in order to check hashes.
			string PwnedHashes = await GetHashes(headhash);

			if((PwnedHashes == "-1") || (String.IsNullOrWhiteSpace(PwnedHashes))) { throw new Exception(); } //No need of Else because throw Exception goes out of the function.

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
	}
}
