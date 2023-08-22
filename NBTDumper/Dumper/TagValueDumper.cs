using fNbt;
using NBTDumper.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTDumper.Dumper
{
    public class TagValueDumper
    {
        /// <summary>
        /// This is meant for tags that have a value, like integers, bytes, strings, etc.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="CurrentDir"></param>
        public static void DumpValueTag(NbtTag tag, string CurrentDir)
        {
            string filename = "";
            if (tag.Name != null)
            {
                if (!DirectoryHelper.CheckIfDirectoryWithTheSameNameExists(tag.Name, CurrentDir))
                {
                    filename = tag.Name;
                }
                else
                {
                    filename = tag.Name + new Random().Next().ToString();
                }
            }
            else
            {
                if (!DirectoryHelper.CheckIfFileWithTheSameNameExists(tag.TagType.ToString(), CurrentDir))
                {
                    filename = tag.TagType.ToString();
                }
                else
                {
                    filename = tag.TagType.ToString() + new Random().Next().ToString();
                }
            }
            using (StreamWriter sw = File.CreateText(CurrentDir + "\\" + filename))
            {
                Log.Verbose("Created file with name: " + filename);
                sw.WriteLine("[TAG NAME]: "+tag.Name);
                sw.WriteLine("[TAG PATH]: " + tag.Path);
                sw.WriteLine("[TAG TYPE]: " + tag.TagType);

                switch (tag.TagType)
                {
                    case NbtTagType.Byte:
                        sw.WriteLine("[TAG VALUE]: " + tag.ByteValue);
                        break;
                    case NbtTagType.Short:
                        sw.WriteLine("[TAG VALUE]: " + tag.ShortValue);
                        break;
                    case NbtTagType.Int:
                        sw.WriteLine("[TAG VALUE]: " + tag.IntValue);
                        break;
                    case NbtTagType.Long:
                        sw.WriteLine("[TAG VALUE]: " + tag.LongValue);
                        break;
                    case NbtTagType.Float:
                        sw.WriteLine("[TAG VALUE]: " + tag.FloatValue);
                        break;
                    case NbtTagType.Double:
                        sw.WriteLine("[TAG VALUE]: " + tag.DoubleValue);
                        break;
                    case NbtTagType.ByteArray:
                        foreach (int value in tag.ByteArrayValue)
                        {
                            sw.WriteLine("[TAG VALUE]: " + value);
                        }
                        break;
                    case NbtTagType.String:
                        sw.WriteLine("[TAG VALUE]: " + tag.StringValue);
                        break;
                    case NbtTagType.IntArray:
                        foreach (int value in tag.IntArrayValue)
                        {
                            sw.WriteLine("[TAG VALUE]: " + value);
                        }
                        break;
                    default:
                        break;
                }

                sw.Close();
            }
        }
    }
}
