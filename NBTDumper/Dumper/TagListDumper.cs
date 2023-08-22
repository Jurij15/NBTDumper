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
    public class TagListDumper
    {
        public static void NbtListTag(NbtList List, string CurrentDir)
        {
            if (List.Name != null)
            {
                CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(List.Name);
            }
            else
            {
                CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(List.Count.ToString());
            }
            Directory.CreateDirectory(CurrentDir);
            Log.Verbose("Created directory:" + CurrentDir);
            foreach (NbtTag tag in List)
            {
                Stats.DumpedTagsTotal++;
                Log.Verbose("Found " + tag.TagType + " in " + List.Path);
                if (tag.TagType == NbtTagType.Compound)
                {
                    //Console.WriteLine(tag.Name);
                    TagCompoundDumper.NbtCompoundTag(tag as NbtCompound, CurrentDir);
                }
                else if (tag.TagType == NbtTagType.List)
                {
                    Console.WriteLine(tag.Name);
                    NbtListTag(tag as NbtList, CurrentDir);
                }
                else
                {
                    TagValueDumper.DumpValueTag(tag, CurrentDir);
                }
            }
            Stats.DumpedTagsTotal++;
        }
    }
}
