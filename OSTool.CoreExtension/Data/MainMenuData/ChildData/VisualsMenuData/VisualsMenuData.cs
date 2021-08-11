using OSTool.Core;
using static OSTool.Core.XmlFile;
using static OSTool.CoreExtension.RegistryChange;

namespace OSTool.CoreExtension
{
    public class VisualsMenuData
    {
        public static object Load(int type, int loadtype)
        {
            xmlWrite("", "", "Visuals", XmlWriteEnums.List);

            var load = new string[]
            {
                type == 0 ? "System/Application Theme" : type == 1 ? "This controls the system/application theme." : type == 2 ? "Application Dark Mode" : "System Dark Mode",
                type == 0 ? "Shell Transparency" : type == 1 ? "This controls the shell transparency parameter." : type == 2 ? "Transparency (Off)" : string.Empty
            };

            return load[loadtype];
        }

        public static bool Visible(int visibletype)
        {
            var load = new bool[]
            {
                true,
                false
            };

            return load[visibletype];
        }

        public static void Execute(object type, int selecttype, int runtype)
        {
            if (type.ToString() == "main")
            {
                if (selecttype == 0) { ApplicationTheme(runtype); }
                if (selecttype == 1) { Transparent(runtype); }
            }
            else
            {
                if (selecttype == 0) { SystemTheme(runtype); }
            }
        }

        #region Execute Data

        public static void ApplicationTheme(int type)
        {
            xmlWrite(nameof(ApplicationTheme), type == 0 ? "True" : "False", "Visuals", XmlWriteEnums.Element);
            RegistryAdd(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", type == 0 ? 0x0 : 0x1, CustomRegistryValueKind.DWord, RegistryBaseKey.User);
        }

        public static void SystemTheme(int type)
        {
            xmlWrite(nameof(SystemTheme), type == 0 ? "True" : "False", "Visuals", XmlWriteEnums.Element);
            RegistryAdd(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", type == 0 ? 0x0 : 0x1, CustomRegistryValueKind.DWord, RegistryBaseKey.User);
        }

        public static void Transparent(int type)
        {
            xmlWrite(nameof(Transparent), type == 0 ? "True" : "False", "Visuals", XmlWriteEnums.Element);
            RegistryAdd(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", type == 0 ? 0x0 : 0x1, CustomRegistryValueKind.DWord, RegistryBaseKey.User);
        }

        #endregion
    }
}