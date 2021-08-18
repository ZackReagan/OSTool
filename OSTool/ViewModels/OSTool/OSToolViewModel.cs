using OSTool.Core;
using System.Windows;

namespace OSTool
{
    public class OSToolViewModel : BaseViewModel
    {
        #region Single

        public static ApplicationViewModel Instance = IoC.Get<ApplicationViewModel>();

        #endregion

        #region Private 

        private Window wWindow;

        private int wOuterMarginSize = 10;
        private int wWindowRadius = 15;

        #endregion

        public OSToolViewModel(Window window)
        {
            wWindow = window;

            wWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            Menu = new BaseCommand(e => ChangeView(e));
            Minimize = new BaseCommand(e => wWindow.WindowState = WindowState.Minimized);
            Maximize = new BaseCommand(e => wWindow.WindowState ^= WindowState.Maximized);
            Close = new BaseCommand(e => CloseMessage());
        }

        #region Voids

        private void ChangeView(object name)
        {
            if (Instance.ButtonName != name)
            {
                Instance.ButtonName = name;
                Instance.FirstTime = false;
                Instance.CurrentView = new ExpanderItemsList();
            }
        }

        private void CloseMessage()
        {
            if (Dialog.Show("Would you like to exit?", DialogBoxType.YesNo) == DialogBoxResult.Yes)
            {
                wWindow.Close();
            }
        }

        #endregion

        #region Commands

        public BaseCommand Menu { get; set; }

        public BaseCommand Minimize { get; set; }

        public BaseCommand Maximize { get; set; }

        public BaseCommand Close { get; set; }

        #endregion

        #region Public

        public int OuterMarginSize
        {
            get
            {
                return wWindow.WindowState == WindowState.Maximized ? 0 : wOuterMarginSize;
            }
            set
            {
                wOuterMarginSize = value;
            }
        }

        public int WindowRadius
        {
            get
            {
                return wWindow.WindowState == WindowState.Maximized ? 0 : wWindowRadius;
            }
            set
            {
                wWindowRadius = value;
            }
        }

        public Thickness ResizeBorderThickness => new Thickness(Instance.ResizeBorder + OuterMarginSize);

        public Thickness OuterMarginThickness => new Thickness(OuterMarginSize);

        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        public GridLength TitleHeightGridLength => new GridLength(Instance.TitleHeight + Instance.ResizeBorder);

        #endregion
    }
}