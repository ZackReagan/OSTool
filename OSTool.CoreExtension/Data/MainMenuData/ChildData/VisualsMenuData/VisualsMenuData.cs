namespace OSTool.CoreExtension
{
    public class VisualsMenuData
    {
        public static object Load(int type, int loadtype)
        {
            var load = new string[]
            {
                type == 0 ? "System/Application Theme" : type == 1 ? "This controls the system/application theme." : type == 2 ? "System Dark Mode" : "Application Dark Mode"
            };

            return load[loadtype];
        }

        public static bool Visible(int visibletype)
        {
            var load = new bool[]
            {
                true
            };

            return load[visibletype];
        }

        public static void Execute(object type, int selecttype, int runtype)
        {
            if (type.ToString() == "main")
            {
                if (selecttype == 0) { DarkOrLight(runtype); }
            }
        }

        #region Execute Data

        public static void DarkOrLight(int type) { }

        #endregion
    }
}