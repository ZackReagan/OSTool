using System;
using OSTool.Core;
using static OSTool.Core.XmlReadWrite;
using static OSTool.CoreExtension.RegistryChange;

namespace OSTool.CoreExtension
{
    public class RegistryMenuData
    {
        public static object Load(int type, int loadtype)
        {
            Write("", "", "Registry", XmlWriteEnums.List);

            var load = new string[]
            {
                type == 0 ? "Execute Options" : type == 1 ? "This controls mitigations and application execute settings." : type == 2 ? "Execute Options (Process Mitigations)" : string.Empty,
                type == 0 ? "Memory Management" : type == 1 ? "This controls memory management related settings." : type == 2 ? "LargeSystemCache (1)" : "Disable Mitigations (Spectre & Meltdown)",
                type == 0 ? "Power Throttling" : type == 1 ? "This controls the power throttling policy setting." : type == 2 ? "Power Throttling (Off)" : string.Empty,
                type == 0 ? "PriorityControl" : type == 1 ? "This controls priority related settings." : type == 2 ? "Win32PrioritySeparation (26)" : string.Empty,
                type == 0 ? "Start Options" : type == 1 ? "This controls BCDedit and logging related settings." : type == 2 ? "BCDedit (PlatformTick)" : "Disable Logging (EventLog, boot logging)",
                type == 0 ? "SvcHost Combining" : type == 1 ? "This controls the SvcHost combining setting." : type == 2 ? "SvcHostSplitThreshold (Combine)" : string.Empty
                
            };

            return load[loadtype];
        }

        public static bool Visible(int visibletype)
        {
            var load = new bool[]
            {
                false,
                true,
                false,
                false,
                true,
                false
            };

            return load[visibletype];
        }

        public static void Execute(string type, int selecttype, string nametype, int runtype)
        {
            if (nametype != null)
            {
                Write(nametype, runtype == 0 ? "True" : "False", "Registry", !Exist("Registry", nametype, XmlExistEnums.Element) ? XmlWriteEnums.Element : XmlWriteEnums.Value);
            }

            if (type == "main")
            {
                if (selecttype == 0) { ExecuteOptions(runtype); }
                if (selecttype == 1) { SystemCache(runtype); }
                if (selecttype == 2) { PwrThrottling(runtype); }
                if (selecttype == 3) { Win32Prio(runtype); }
                if (selecttype == 4) { StartOptions(runtype); }
                if (selecttype == 5) { SvcHostSplit(runtype); }
            }
            else
            {
                if (selecttype == 1) { Mitigations(runtype); }
                if (selecttype == 4) { SystemLogging(runtype); }
            }
        }

        #region Execute Data

        public static void ExecuteOptions(int type)
        {
            ProcessStart.Process(@"C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe", "powershell", type == 0 ? new string[] { "Set-ProcessMitigation -System -Disable DEP, SEHOP, AuditSEHOP, SEHOPTelemetry, CFG" } : new string[] { "Set-ProcessMitigation -System -Enable DEP, SEHOP, AuditSEHOP, SEHOPTelemetry, CFG" }); 
        }

        public static void Mitigations(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettings", type == 0 ? 0x0 : 0x1, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverride", type == 0 ? 0x3 : 0x0, CustomRegistryValueKind.DWord);
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverrideMask", type == 0 ? 0x3 : 0x0, CustomRegistryValueKind.DWord);
        }

        public static void PwrThrottling(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Power\PowerThrottling", "PowerThrottlingOff", type == 0 ? 0x1 : 0x0, CustomRegistryValueKind.DWord);
        }

        public static void StartOptions(int type)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                ProcessStart.Process(!Environment.Is64BitProcess ? @"C:\Windows\Sysnative\bcdedit.exe" : @"C:\Windows\System32\bcdedit.exe", !Environment.Is64BitProcess ? @"C:\Windows\Sysnative\bcdedit.exe" : "bcdedit.exe", new string[] { type == 0 ? "/set useplatformtick yes" : "/deletevalue useplatformtick", type == 0 ? "/set disabledynamictick yes" : "/deletevalue disabledynamictick" });
            }
            else { Dialog.Show("Process start failed", "BCDedit can't be modified on a 32 bit OS."); }
        }

        public static void SvcHostSplit(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control", "SvcHostSplitThresholdInKB", type == 0 ? ManagementObject.Query("SELECT * FROM Win32_OperatingSystem", "TotalVisibleMemorySize") : 0x380000, CustomRegistryValueKind.DWord);
        }

        public static void SystemCache(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "LargeSystemCache", type == 0 ? 0x1 : 0x0, CustomRegistryValueKind.DWord); 
        }

        public static void SystemLogging(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Services\EventLog", "Start", type == 0 ? 0x4 : 0x2, CustomRegistryValueKind.DWord);

            using (var key = RegsitryAddMultiple(@"SYSTEM\CurrentControlSet\Services\NlaSvc"))
            {
                key.SetValue("DependOnService", type == 0 ? new string[] { "NSI", "RpcSs", "TcpIp", "Dhcp" } : new string[] { "NSI", "RpcSs", "TcpIp", "Dhcp", "EventLog" }, CustomKindToRegistryKind.CustomToRegsitry(CustomRegistryValueKind.MultiString));
            }
        }

        public static void Win32Prio(int type)
        {
            RegistryAdd(@"SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", type == 0 ? 0x26 : 0x2, CustomRegistryValueKind.DWord);
        }

        #endregion
    }
}