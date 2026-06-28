using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	/// <summary>
	/// Static Utils class for methods that involve Boolean handling.
	/// </summary>
	internal static class BooleanUtils
	{
		/// <summary>
		/// Given an RGB, checks its lightness following HSL format, and if it is >= 20 and <= 90 then it is valid colour, otherwise it is too dark or bright.
		/// 
		/// We will check if the color is valid by calcutating its lightness (the L in HSL), and if it is below 20% then it is too dark.
		//		L(%) = ((max(r, g, b) / 2) / 255) * 100  --> https://en.wikipedia.org/wiki/HSL_and_HSV
		//												 --> https://es.wikipedia.org/wiki/Modelo_de_color_HSL
		/// 
		/// </summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		/// <returns></returns>
		internal static bool IsValidColour(int red, int green, int blue)
		{
			

			float max = Math.Max(red, Math.Max(green, blue));
			float min = Math.Min(red, Math.Min(green, blue));

			float lightnessPercentage = ((((max+min) / 2) / 255) * 100);

			return (lightnessPercentage >= 20) && (lightnessPercentage <= 90);
		}
	}
}
