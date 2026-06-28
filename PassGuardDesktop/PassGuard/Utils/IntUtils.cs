using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	/// <summary>
	/// Static Utils class for methods that involve Int handling.
	/// </summary>
	internal static class IntUtils
	{
		/// <summary>
		/// Returns an RGB config calibrated for the three panels.
		/// Order of the colors are: ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptions, GOptions, BOptions
		/// </summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		/// <returns></returns>
		internal static int[] CalibrateAllColours(int red, int green, int blue)
		{
			return new List<int>()
				{
					Math.Min((red + 10), 255),
					Math.Min((green + 10), 255),
					Math.Min((blue + 10), 255),
					red,
					green,
					blue,
					red,
					green,
					blue
				}.ToArray();
		}

		/// <summary>
		/// Given RGB config, it returns the complementary RGB.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		internal static int[] GetComplementaryRGB(int r, int g, int b)
		{
			return new int[] { 255 - r, 255 - g, 255 - b};
		}
	}
}
