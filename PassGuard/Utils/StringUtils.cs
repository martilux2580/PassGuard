using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.Utils
{
    internal static class StringUtils
    {
        //Function to check if a string has Lower, Upper, Number or Symbol characters, based on those modes (Lower, Upper, Number, Symbol).
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

        //Returns a String in Default Encoding given a src string in base64.
        internal static String Base64ToString(String src)
        {
            var base64EncodedBytes = Convert.FromBase64String(src);
            return Encoding.Default.GetString(base64EncodedBytes);
        }
        

    }
}
