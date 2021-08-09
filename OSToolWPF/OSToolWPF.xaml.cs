using System.Windows;

namespace OSTool
{
    public partial class OSToolWPF : Window
    {
        public OSToolWPF()
        {
            InitializeComponent();
            DataContext = new OSToolViewModel(this);
        }
    }
}