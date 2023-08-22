using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTDumper.Helpers
{
    public class DirectoryHelper
    {
        public static bool CheckIfDirectoryWithTheSameNameExists(string FileOrDirName, string DirectoryToCheck)
        {
            bool ret = false;
            foreach (var item in Directory.GetDirectories(DirectoryToCheck))
            {
                if (item.Contains(FileOrDirName, StringComparison.OrdinalIgnoreCase))
                {
                    ret = true;
                }
            }

            return ret;
        }
        public static bool CheckIfFileWithTheSameNameExists(string FileOrDirName, string DirectoryToCheck)
        {
            bool ret = false;
            foreach (var item in Directory.GetFiles(DirectoryToCheck))
            {
                if (item.Contains(FileOrDirName, StringComparison.OrdinalIgnoreCase))
                {
                    ret = true;
                }
            }

            return ret;
        }
    }
}
