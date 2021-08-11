using Microsoft.Win32;

namespace OSTool.CoreExtension
{
    public class RegistryChange
    {
        public static void RegistryAdd(string regloc, string regkey, object regvalue, CustomRegistryValueKind regkind, RegistryBaseKey basekey = RegistryBaseKey.LocalMachine)
        {
            var key = RegsitryAddMultiple(regloc, basekey);
            key.SetValue(regkey, regvalue, CustomKindToRegistryKind.CustomToRegsitry(regkind));
        }

        public static RegistryKey RegsitryAddMultiple(string subkey, RegistryBaseKey basekey = RegistryBaseKey.LocalMachine)
        {
            if (basekey == RegistryBaseKey.LocalMachine)
            {
                var key = Registry.LocalMachine.OpenSubKey(subkey, true);
                return key ?? Registry.LocalMachine.CreateSubKey(subkey);
            }
            else
            {
                var key = Registry.CurrentUser.OpenSubKey(subkey, true);
                return key ?? Registry.CurrentUser.CreateSubKey(subkey);
            }
        }

        public static void RegistryRemove(string regkey, bool missingsubkey)
        {
            Registry.LocalMachine.DeleteSubKeyTree(regkey, missingsubkey);
        }
    }
}