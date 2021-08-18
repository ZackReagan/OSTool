using System;
using OSTool.Core;
using System.Windows;
using static OSTool.Core.IoC;
using static OSTool.Core.XmlReadWrite;

namespace OSTool
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationSetup();

            Current.MainWindow = new OSToolWindow();
            Current.MainWindow.Show();
        }

        private void ApplicationSetup()
        {
            #region IoC Setup

            Setup();
            IoCContainer.RegisterSingleType<IDialogService, DialogService>();

            #endregion

            #region Xml Create & Integrity Check

            Create();

            #endregion

            #region IoC FirstTime

            Get<ApplicationViewModel>().FirstTime = Convert.ToBoolean(Read("FirstTime"));

            #endregion
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            #region FirstTime Check

            try
            {
                if (Read("FirstTime") == "True") { Write("FirstTime", "False"); }
            }

            catch { }

            #endregion
        }
    }
}