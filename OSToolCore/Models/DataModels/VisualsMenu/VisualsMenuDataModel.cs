using static OSTool.Core.VisualsExecuteData;

namespace OSTool.Core
{
    public class VisualsMenuDataModel
    {
        public static void Execute(int type)
        {
            if (type == 0) { DarkOrLight(); }
            if (type == 1) { AccentColor(); }
            if (type == 2) { BackgroundColor(); }
        } 
    }

    public class VisualsExecuteData
    {
        #region Void

        public static void DarkOrLight() { }

        public static void AccentColor() { }

        public static void BackgroundColor() { }

        #endregion
    }
}