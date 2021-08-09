using System;
using static OSTool.Core.RegistryChange;
using static OSTool.Core.RegistryExecuteData;

namespace OSTool.Core
{
    public class RegistryMenuDataModel
    {
        public static void Execute(int type)
        {
            if (type == 0) { Win32Prio(); }
            if (type == 1) { SystemCache(); }
            if (type == 2) { LargePage(); }
            if (type == 3) { Mitigations(); }
            if (type == 4) { PwrThrottling(); }
            if (type == 5) { EventLog(); }
            if (type == 6) { StartOptions(); }
            if (type == 7) { SvcHostSplit(); }
            if (type == 8) { ExecuteOptions(); }
        }     
    }

    public class RegistryExecuteData
    {
        #region Voids

        public static void Win32Prio() { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", 0x26, CustomRegistryValueKind.DWord); }

        public static void LargePage() { }

        public static void SystemCache() { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "LargeSystemCache", 0x1, CustomRegistryValueKind.DWord); }

        public static void Mitigations()
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettings", 0x0, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverride", 0x3, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverrideMask", 0x3, CustomRegistryValueKind.DWord);
        }

        public static void PwrThrottling() { RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Power\PowerThrottling", "PowerThrottlingOff", 0x1, CustomRegistryValueKind.DWord); }

        public static void EventLog()
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Services\EventLog", "Start", 0x4, CustomRegistryValueKind.DWord);

            using (var key = RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services\NlaSvc"))
            {
                key.SetValue("DependOnService", new string[] { "NSI", "RpcSs", "TcpIp", "Dhcp" }, CustomKindToRegistryKind.CustomToRegsitry(CustomRegistryValueKind.MultiString));
            }
        }

        public static void StartOptions() { ProcessStart.Process(@"C:\Windows\System32\cmd.exe", "bcdedit", new string[] { "/set useplatformtick yes", "/set disabledynamictick yes" }, false); }

        public static void SvcHostSplit()
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control", "SvcHostSplitThresholdInKB", Convert.ToInt32(ManagementObject.Query("SELECT * FROM Win32_OperatingSystem", "TotalVisibleMemorySize")), CustomRegistryValueKind.DWord);
        }

        public static void ExecuteOptions() { ProcessStart.Process(@"C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe", "powershell", new string[] { "Set-ProcessMitigation -System -Disable DEP, SEHOP, AuditSEHOP, SEHOPTelemetry, CFG" }, false); }

        #endregion
    }
}