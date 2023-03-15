using System.Collections.Generic;

namespace HumWebAPI3.Models
{
    public static class StringCleaner
    {
        public static string StrCln(this string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }
            return str;
        }
    }
}