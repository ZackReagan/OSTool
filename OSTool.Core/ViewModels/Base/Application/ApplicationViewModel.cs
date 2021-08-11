namespace OSTool.Core
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private

        private object wButtonName;
        private object wCurrentView;

        #endregion

        #region Public

        public int ResizeBorder { get; set; } = 6;

        public double TitleHeight { get; set; } = 50;

        public object ButtonName
        {
            get { return wButtonName; }

            set
            {
                if (value == wButtonName)
                    return;

                wButtonName = value;
                OnPropertyChanged(nameof(ButtonName));
            }
        }

        public object CurrentView
        {
            get { return wCurrentView; }

            set
            {
                if (value == wCurrentView)
                    return;

                wCurrentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        #endregion
    }
}