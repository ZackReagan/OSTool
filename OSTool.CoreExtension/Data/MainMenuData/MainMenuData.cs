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

        public static void Execute(object type, object objtype, int selecttype, object text = null, int runtype = 0)
        {
            if (type.ToString() == "Registry") { RegistryMenuData.Execute(objtype, selecttype, runtype); }
            else if (type.ToString() == "Services") { ServicesMenuData.Execute(objtype, text, runtype); }
            else { VisualsMenuData.Execute(objtype, selecttype, runtype); }
        }

        public static object Load(object type, int txttype, int loadtype)
        {
            return type.ToString() == "Registry" ? RegistryMenuData.Load(txttype, loadtype) :
                   type.ToString() == "Services" ? ServicesMenuData.Load(txttype, loadtype) :
                   VisualsMenuData.Load(txttype, loadtype);
        }
    }
}