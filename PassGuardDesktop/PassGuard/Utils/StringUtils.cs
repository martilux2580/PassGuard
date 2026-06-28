using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	/// <summary>
	/// Static Utils class for methods that involve String handling.
	/// </summary>
	internal static class StringUtils
	{
		/// <summary>
		/// Function to check if a string has Lower, Upper, Number or Symbol characters, based on those modes (Lower, Upper, Number, Symbol).
		/// </summary>
		/// <param name="str"></param>
		/// <param name="mode">Can be Lower, Upper, Number or Symbol</param>
		/// <returns></returns>
		internal static bool Check(String str, String mode)
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

		/// <summary>
		/// Returns a String in Default Encoding given a src string in base64.
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		internal static String Base64ToString(String src)
		{
			var base64EncodedBytes = Convert.FromBase64String(src);
			return Encoding.Default.GetString(base64EncodedBytes);
		}
		

	}
}
