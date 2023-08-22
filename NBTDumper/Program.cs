using fNbt;
using NBTDumper.Dumper;
using NBTDumper.Helpers;
using NBTDumper.Json;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection.Emit;

namespace NBTDumper
{
    internal class Program
    {
        static string FilePath { get;set; }
        static void GetPath()
        {
            if (!File.Exists("config.json"))
            {
                var json = JsonConvert.SerializeObject(new Config());
                using (StreamWriter sw = File.CreateText("config.json"))
                {
                    sw.Write(json);
                    sw.Close();
                }
                Console.WriteLine("Config file created, please check it and restart the dumper!");
                Log.Information("Config file created, please check it and restart the dumper!");

                Log.CloseAndFlush();
                Console.ReadLine();
            }
            else
            {
                Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
                Log.Verbose("Deserialized config.json");

                FilePath = config.FilePath;
                Log.Information("Found path!");
            }
        }

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.File(StringHelper.RemoveInvalidFileCharacters("DumperLog" + DateTime.Now.ToString()+".log"), outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}", restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            Log.Information("NBTDumper by Jurij15, Version: "+Version.VersionString);

            GetPath();

            LoadDumper();

            Log.Information("Dumping Finished!");

            Log.Information("   ");
            Log.Information("Dumped:");
            Log.Information("Total Tags: "+ Stats.DumpedTagsTotal);
            Log.Information("   ");

            Log.Information("You can now close the window.");

            Log.CloseAndFlush();

            Console.ReadLine();
        }


        #region Dumper
        static void LoadDumper()
        {
            Log.Information("Loading dumper...");
            string CurrentDir;
            NbtFile nbtfile = new NbtFile();
            nbtfile.LoadFromFile(FilePath);

            NbtCompound RootCompound;

            if (nbtfile.RootTag != null)
            {
                CurrentDir = Path.GetFileNameWithoutExtension(FilePath);
                RootCompound = nbtfile.RootTag as NbtCompound;

                Directory.CreateDirectory(CurrentDir);
                Log.Verbose("Created directory with name: " + CurrentDir);

                Log.Information("Dumping started!");
                foreach (NbtTag tag in RootCompound.Tags)
                {
                    if (tag.TagType == NbtTagType.Compound)
                    {
                        TagCompoundDumper.NbtCompoundTag(tag as NbtCompound, CurrentDir);
                    }
                }
            }
        }
        #endregion
    }
}