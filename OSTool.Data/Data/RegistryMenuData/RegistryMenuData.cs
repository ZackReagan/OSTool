using System;
using OSTool.Core;
using static OSTool.Data.RegistryChange;

namespace OSTool.Data
{
    public class RegistryMenuData
    {
        public static string Load(int type, int loadtype)
        {
            var load = new string[]
            {
                type == 0 ? "PriorityControl" : "This controls priority related settings.",
                type == 0 ? "Memory Management" : "This controls memory management related settings.",
                type == 0 ? "Power Throttling" : "This controls the power throttling policy setting.",
                type == 0 ? "Start Options" : "This controls BCDedit and logging related settings.",
                type == 0 ? "SvcHost Combining" : "This controls the SvcHost combining setting.",
                type == 0 ? "Execute Options" : "This controls mitigations and application execute settings.",
            };

            return load[loadtype];
        }

        public static int Execute(object type = null, int runtype = 0)
        {
            if (Convert.ToInt32(type) == 0) { Win32Prio(runtype); }
            if (Convert.ToInt32(type) == 1) { SystemCache(runtype); }
            if (Convert.ToInt32(type) == 2) { Mitigations(runtype); }
            if (Convert.ToInt32(type) == 3) { PwrThrottling(runtype); }
            if (Convert.ToInt32(type) == 4) { EventLog(runtype); }
            if (Convert.ToInt32(type) == 5) { StartOptions(runtype); }
            if (Convert.ToInt32(type) == 6) { SvcHostSplit(runtype); }
            if (Convert.ToInt32(type) == 7) { ExecuteOptions(runtype); }

            return 5;
        }

        #region Execute Data

        public static void Win32Prio(int runtype) { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", runtype == 0 ? 0x26 : 0x2, CustomRegistryValueKind.DWord); }

        public static void SystemCache(int runtype) { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "LargeSystemCache", 0x1, CustomRegistryValueKind.DWord); }

        public static void Mitigations(int runtype)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettings", 0x0, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverride", 0x3, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverrideMask", 0x3, CustomRegistryValueKind.DWord);
        }

        public static void PwrThrottling(int runtype) { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Power\PowerThrottling", "PowerThrottlingOff", 0x1, CustomRegistryValueKind.DWord); }

        public static void EventLog(int runtype)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Services\EventLog", "Start", 0x4, CustomRegistryValueKind.DWord);

            using (var key = RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services\NlaSvc"))
            {
                key.SetValue("DependOnService", new string[] { "NSI", "RpcSs", "TcpIp", "Dhcp" }, CustomKindToRegistryKind.CustomToRegsitry(CustomRegistryValueKind.MultiString));
            }
        }

        public static void StartOptions(int runtype) { ProcessStart.Process(@"C:\Windows\System32\cmd.exe", "bcdedit", new string[] { "/set useplatformtick yes", "/set disabledynamictick yes" }, false); }

        public static void SvcHostSplit(int runtype)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control", "SvcHostSplitThresholdInKB", Convert.ToInt32(ManagementObject.Query("SELECT * FROM Win32_OperatingSystem", "TotalVisibleMemorySize")), CustomRegistryValueKind.DWord);
        }

        public static void ExecuteOptions(int runtype) { ProcessStart.Process(@"C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe", "powershell", new string[] { "Set-ProcessMitigation -System -Disable DEP, SEHOP, AuditSEHOP, SEHOPTelemetry, CFG" }, false); }

        #endregion
    }
}