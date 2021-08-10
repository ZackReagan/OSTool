namespace OSTool.Data
{
    public class VisualsMenuData
    {
        public static string Load(int type)
        {
            return type == 0 ?
                "" : type == 1 ?
                "" : "";
        }

        public static void Execute(int type)
        {
            if (type == 0) { DarkOrLight(); }
            if (type == 1) { AccentColor(); }
            if (type == 2) { BackgroundColor(); }
        }

        #region Execute Data

        public static void DarkOrLight() { }

        public static void AccentColor() { }

        public static void BackgroundColor() { }

        #endregion
    }
}