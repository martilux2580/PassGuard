using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
	internal static class IntUtils
	{
		internal static int[] CalibrateAllColours(int red, int green, int blue)
		{
			//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
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
	}
}
