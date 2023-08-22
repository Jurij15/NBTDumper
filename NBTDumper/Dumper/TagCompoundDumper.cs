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
    public class TagCompoundDumper
    {
        public static void NbtCompoundTag(NbtCompound Compound, string CurrentDir)
        {
            if (Compound.Name != null)
            {
                if (!DirectoryHelper.CheckIfDirectoryWithTheSameNameExists(Compound.Name, CurrentDir))
                {
                    CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(Compound.Name);
                }
                else
                {
                    CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(Compound.Name + new Random().Next().ToString());
                }
            }
            else
            {
                if (!DirectoryHelper.CheckIfDirectoryWithTheSameNameExists(Compound.Count.ToString(), CurrentDir))
                {
                    CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(Compound.Count.ToString());
                }
                else
                {
                    CurrentDir += "/" + StringHelper.RemoveInvalidFileCharacters(Compound.Count.ToString() + new Random().Next().ToString());
                }
            }
            Directory.CreateDirectory(CurrentDir);
            Log.Verbose("Created directory:" + CurrentDir);
            foreach (NbtTag tag in Compound.Tags)
            {
                Stats.DumpedTagsTotal++;
                Log.Verbose("Found " + tag.TagType + " in " + Compound.Path);
                if (tag.TagType == NbtTagType.Compound)
                {
                    //Console.WriteLine(tag.Name);
                    NbtCompoundTag(tag as NbtCompound, CurrentDir);
                }
                else if (tag.TagType == NbtTagType.List)
                {
                    TagListDumper.NbtListTag(tag as NbtList, CurrentDir);
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
