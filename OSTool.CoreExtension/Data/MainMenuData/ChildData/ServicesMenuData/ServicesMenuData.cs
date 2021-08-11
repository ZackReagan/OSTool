using System;
using System.Linq;
using OSTool.Core;
using Microsoft.Win32;
using static OSTool.Core.XmlFile;

namespace OSTool.CoreExtension
{
    public class ServicesMenuData
    {
        #region References

        public static RegistryKey svc = RegistryChange.RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services");
        public static string[] filter = new string[] { "ahcache", "AmdPPM", "intelppm", "Beep", "CLFS", "cdrom", "NdisVirtualBus", "CompositeBus", "GpuEnergyDrv", "Mpsdrv", "pcw", "QWAVEdrv", "UMBus", "vwifibus", "vwififlt", "vdrvroot", "wanarp", "Wdboot", "WdFilter", "WdNisDrv", "Wdnsfltr", "udfs", "TPM" };
        public static string[] items = svc.GetSubKeyNames().Where(a => filter.Any(b => a.Contains(b))).ToArray();

        #endregion

        public static int Count()
        {
            return items.Count() - 1;
        }

        public static object Load(int type, int loadtype)
        {
            string key = svc.OpenSubKey(items[loadtype]).GetValue("Start").ToString();

            #region Xml Load

            xmlWrite("", "", "Services", XmlWriteEnums.List);

            if (!xmlExist("Services", items[loadtype], XmlExistEnums.Element))
            {
                xmlWrite(items[loadtype], key, "Services", XmlWriteEnums.Element);
            }
            else
            {
                xmlWrite(items[loadtype], key, "Services", XmlWriteEnums.Element);

                if (!xmlRead(items[loadtype]).Contains(":Disabled"))
                {
                    xmlWrite(items[loadtype], key, "Services", XmlWriteEnums.Value);
                }
            }

            #endregion

            return type == 0 ? items[loadtype] : type == 1 ? string.Empty : type == 2 ? "Start (4)" : string.Empty;
        }

        public static bool Visible()
        {
            return false;
        }

        public static void Execute(object type, object text, int runtype)
        {
            if (type.ToString() == "main")
            {
                Start(text.ToString(), runtype);
            }
        }

        #region Execute Data

        public static void Start(string text, int type)
        {
            xmlWrite(text, type == 0 ? xmlRead(text) + ":Disabled" : StringOrInt.Split(xmlRead(text))[1], "Services", XmlWriteEnums.Value);
            RegistryChange.RegistryAdd(@"SYSTEM\CurrentControlSet\Services\" + text, "Start", type == 0 ? 0x4 : Convert.ToInt32(StringOrInt.Split(xmlRead(text))[1]), CustomRegistryValueKind.DWord);
        }

        #endregion
    }
}