namespace OSTool.CoreExtension
{
    public class MainMenuData
    {
        public static int Count(object type)
        {
            return type.ToString() == "Registry" ? 5 :
                   type.ToString() == "Services" ? ServicesMenuData.Count() :
                   1;
        }

        public static bool Visible(object type, int visibletype)
        {
            return type.ToString() == "Registry" ? RegistryMenuData.Visible(visibletype) :
                   type.ToString() == "Services" ? ServicesMenuData.Visible() :
                   VisualsMenuData.Visible(visibletype);
        }

        public static void Execute(string type, string checktype, int selecttype, string nametype, int runtype = 0)
        {
            if (type == "Registry") { RegistryMenuData.Execute(checktype, selecttype, nametype, runtype); }
            else if (type == "Services") { ServicesMenuData.Execute(checktype, nametype, runtype); }
            else { VisualsMenuData.Execute(checktype, selecttype, nametype, runtype); }
        }

        public static object Load(object type, int txttype, int loadtype)
        {
            return type.ToString() == "Registry" ? RegistryMenuData.Load(txttype, loadtype) :
                   type.ToString() == "Services" ? ServicesMenuData.Load(txttype, loadtype) :
                   VisualsMenuData.Load(txttype, loadtype);
        }
    }
}