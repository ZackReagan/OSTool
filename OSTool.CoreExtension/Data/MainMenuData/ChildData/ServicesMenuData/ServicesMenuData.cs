using System;
using System.Linq;
using OSTool.Core;
using Microsoft.Win32;
using static OSTool.Core.XmlReadWrite;

namespace OSTool.CoreExtension
{
    public class ServicesMenuData
    {
        #region References

        public static RegistryKey svc = RegistryChange.RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services");
        public static string[] items = svc.GetSubKeyNames().Where(a => new string[] { "ahcache", "AmdPPM", "intelppm", "Beep", "CLFS", "cdrom", "NdisVirtualBus", "CompositeBus", "GpuEnergyDrv", "Mpsdrv", "pcw", "QWAVEdrv", "UMBus", "vwifibus", "vwififlt", "vdrvroot", "wanarp", "Wdboot", "WdFilter", "WdNisDrv", "Wdnsfltr", "udfs", "TPM" }.Any(b => a.Contains(b))).ToArray();

        #endregion

        public static int Count()
        {
            return items.Count() - 1;
        }

        public static object Load(int type, int loadtype)
        {
            string key = svc.OpenSubKey(items[loadtype]).GetValue("Start").ToString();

            #region Xml Load

            Write("", "", "Services", XmlWriteEnums.List);

            if (!Exist("Services", items[loadtype], XmlExistEnums.Element))
            {
                Write(items[loadtype], key, "Services", XmlWriteEnums.Element);
            }
            else
            {
                Write(items[loadtype], key, "Services", XmlWriteEnums.Element);

                if (!Read(items[loadtype]).Contains(":Disabled"))
                {
                    Write(items[loadtype], key);
                }
            }

            #endregion

            return type == 0 ? items[loadtype] : type == 1 ? string.Empty : type == 2 ? "Start (4)" : string.Empty;
        }

        public static bool Visible()
        {
            return false;
        }

        public static void Execute(string type, string nametype, int runtype)
        {
            if (type == "main")
            {
                Start(runtype, nametype);
            }
        }

        #region Execute Data

        public static void Start(int type, string nametype)
        {
            RegistryChange.RegistryAdd(@"SYSTEM\CurrentControlSet\Services\" + nametype, "Start", type == 0 ? 0x4 : Convert.ToInt32(StringOrInt.Split(Read(nametype))[0].Trim(':')), CustomRegistryValueKind.DWord);
            Write(nametype, type == 0 ? Read(nametype) + ":Disabled" : StringOrInt.Split(Read(nametype))[0].Trim(':'), "Services", XmlWriteEnums.Value);
        }

        #endregion
    }
}