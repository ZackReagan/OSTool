using System.Windows;

namespace OSTool
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Current.MainWindow = new OSToolWindow();
            Current.MainWindow.Show();
        }
    }
}