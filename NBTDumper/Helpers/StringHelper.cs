using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NBTDumper.Helpers
{
    public static class StringHelper
    {
        public static string RemoveInvalidFileCharacters(string input)
        {
            if (input == null)
            {
                return null;
            }
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            string pattern = $"[{Regex.Escape(invalidChars)}]";
            return Regex.Replace(input, pattern, "");
        }
    }
}
