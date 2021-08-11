using System.Collections.Generic;
using System.Linq;

namespace OSTool.CoreExtension
{
    public class ServicesMenuData
    {
        public static int Count()
        {
            List<string> items = new List<string>();
            List<string> filter = new List<string>() { "ahcache", "AmdPPM", "intelppm", "Beep", "CLFS", "cdrom", "NdisVirtualBus", "CompositeBus", "GpuEnergyDrv", "Mpsdrv", "pcw", "QWAVEdrv", "UMBus", "vwifibus", "vwififlt", "vdrvroot", "wanarp", "Wdboot", "WdFilter", "WdNisDrv", "Wdnsfltr", "udfs", "TPM" };

            foreach (var item in RegistryChange.RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services").GetSubKeyNames().Where(a => filter.Any(b => a.Contains(b))))
            {
                items.Add(item);
            }

            return items.Count - 1;
        }

        public static object Load(int type, int loadtype)
        {
            List<string> items = new List<string>();
            List<string> filter = new List<string>() { "ahcache", "AmdPPM", "intelppm", "Beep", "CLFS", "cdrom", "NdisVirtualBus", "CompositeBus", "GpuEnergyDrv", "Mpsdrv", "pcw", "QWAVEdrv", "UMBus", "vwifibus", "vwififlt", "vdrvroot", "wanarp", "Wdboot", "WdFilter", "WdNisDrv", "Wdnsfltr", "udfs", "TPM" };

            foreach (var item in RegistryChange.RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services").GetSubKeyNames().Where(a => filter.Any(b => a.Contains(b))))
            {
                items.Add(item);
            }

            return type == 0 ? items[loadtype] : type == 1 ? string.Empty : type == 2 ? "Start (4)" : string.Empty;
        }

        public static bool Visible()
        {
            return false;
        }

        public static void Execute(object type, int selecttype, int runtype)
        {
            if (type.ToString() == "main")
            {
                if (selecttype == 0) { Start(runtype); }
            }
        }

        #region Execute Data

        public static void Start(int type) {  }

        #endregion
    }
}