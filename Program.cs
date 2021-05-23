using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SharpGetInstallSoft
{
    class Program
    {
        static void Main(string[] args)
        {

            string sys_key = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            string sys_86_key = @"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            List<Soft> soft_infos = GetInfo(sys_key);
            List<Soft> soft_x86_infos = GetInfo(sys_86_key);

            // Console.WriteLine(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString());

            if (Is64Bit())
            {
                Console.WriteLine("\r\nCurrent System ====> amd64 !!!");
                Console.WriteLine("\r\n=====================================================================x64 Soft=====================================================================\r\n");
                PrintInfo(soft_infos);

                Thread.Sleep(1000);

                Console.WriteLine("\r\n\n=====================================================================x86 Soft=====================================================================\r\n");
                PrintInfo(soft_x86_infos);
                return;

            }
            else
            {
                Console.WriteLine("Current System ====> i386 !!!");
                Console.WriteLine("\r\n=====================================================================x86 Soft=====================================================================\r\n");
                PrintInfo(soft_infos);
            }


        }


        /*
            * DisplayIcon
            * DisplayName
            * DisplayVersion
            * InstallLocation
            * InstallDate
            * InstallSource
            * Publisher
            */
        public static List<Soft> GetInfo(string SubKey)
        {

            List<Soft> softs = new List<Soft>();

            RegistryKey registry;
            registry = Registry.LocalMachine;
            string[] items = registry.OpenSubKey(SubKey).GetSubKeyNames();

            foreach (string item in items)
            {
                Soft soft = new Soft();
                string softkey = string.Format("{0}{1}{2}", SubKey, "\\", item);

                string[] collects = { "DisplayIcon", "DisplayName", "DisplayVersion", "InstallLocation", "InstallDate", "InstallSource", "Publisher" };

                soft.Icon = NullStr(registry, softkey, collects[0]);
                soft.Name = NullStr(registry, softkey, collects[1]);
                soft.Version = NullStr(registry, softkey, collects[2]);
                soft.InstallLocation = NullStr(registry, softkey, collects[3]);
                soft.InstallDate = NullStr(registry, softkey, collects[4]);
                soft.InstallSource = NullStr(registry, softkey, collects[5]);
                soft.Publisher = NullStr(registry, softkey, collects[6]);
                softs.Add(soft);
            }

            registry.Close();

            return softs;


        }


        public static string NullStr(RegistryKey registry, string softkey, string it)
        {
            var obj = registry.OpenSubKey(softkey).GetValue(it);
            it = obj == null ? string.Empty : obj.ToString();
            return it;
        }


        public static void PrintInfo(List<Soft> softs)
        {
            foreach (Soft soft in softs)
            {
                Console.WriteLine("DisplayIcon: {0}", soft.Icon);
                Console.WriteLine("DisplayName: {0}", soft.Name);
                Console.WriteLine("DisplayVersion: {0}", soft.Version);
                Console.WriteLine("InstallLocation: {0}", soft.InstallLocation);
                Console.WriteLine("InstallDate: {0}", soft.InstallDate);
                Console.WriteLine("InstallSource: {0}", soft.InstallSource);
                Console.WriteLine("Publisher: {0}", soft.Publisher);
                Console.WriteLine("\r\n=====================================================================================================\r\n");
            }

        }


        public static bool Is64Bit()
        {
            bool is64Bit = true;

            if (IntPtr.Size == 4)
                is64Bit = false;

            return is64Bit;
        }

    }
}
