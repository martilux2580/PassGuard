using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
    internal static class IntUtils
    {
        internal static int[] calibrateColours(int red, int green, int blue)
        {
            //ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
            int[] result;

            if ((red > 235) && (green > 235) && (blue > 235))
            {
                result = new int[9] { 245, 245, 245, 255, 255, 255, 250, 250, 250};
                
            }
            else
            {
                if ((red > 235) || (green > 235) || (blue > 235))
                {
                    if (red > 235)
                    {
                        red = 235;
                    }
                    if (green > 235)
                    {
                        green = 235;
                    }
                    if (blue > 235)
                    {
                        blue = 235;
                    }
                }
                result = new int[9] { red+20, green+20, blue+20, red, green, blue, red+10, green+10, blue+10 };
            }

            return result;
        }
    }
}
