using System.Windows;

namespace OSTool
{
    public partial class OSToolWindow : Window
    {
        public OSToolWindow()
        {
            InitializeComponent();
            DataContext = new OSToolViewModel(this);
        }
    }
}