using Microsoft.Win32;

namespace OSTool.CoreExtension
{
    public class RegistryChange
    {
        public static void RegistryAdd(string regloc, string regkey, int regvalue, CustomRegistryValueKind regkind)
        {
            var key = RegsitryAddMultiple(regloc);
            key.SetValue(regkey, regvalue, CustomKindToRegistryKind.CustomToRegsitry(regkind));
        }

        public static RegistryKey RegsitryAddMultiple(string subkey)
        {
            var key = Registry.LocalMachine.OpenSubKey(subkey, true);
            return key != null ? key : Registry.LocalMachine.CreateSubKey(subkey);
        }

        public static void RegistryRemove(string regkey, bool missingsubkey)
        {
            Registry.LocalMachine.DeleteSubKeyTree(regkey, missingsubkey);
        }
    }
}