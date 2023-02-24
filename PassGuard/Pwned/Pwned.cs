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
	internal static class Pwned
	{
		//Obtain all the hashes that start with headHash from an API of pwned passwords.
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
				return "";
			}

		}

		//Compound the SHA1 of password, get all the hashes with a headhash, and if the compound is in the hashes returns true as password has been pwned, if not returns false
		internal static async Task<bool> CheckPwnage(string password)
		{
			IHash sha1 = new SHA1Algorithm();

			string hash = sha1.Compute(password, 160); //Compute SHA1
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
	}
}
