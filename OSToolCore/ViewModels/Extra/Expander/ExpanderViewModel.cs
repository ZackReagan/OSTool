namespace OSTool.Core
{
    public class ExpanderViewModel : BaseViewModel
    {
        #region Private

        private object wCaption;

        private object wInfo;

        private object wMainSelected = "False";

        private object wSecondarySelected = "False";

        private object wControlTag;

        private object wMainTag;

        private object wSecondaryTag;

        #endregion

        #region Commands

        public BaseCommand ButtonCommand { get; set; }

        #endregion

        #region Public

        public object Caption
        {
            get { return wCaption; }

            set
            {
                if (value == wCaption)
                    return;

                wCaption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public object Info
        {
            get { return wInfo; }

            set
            {
                if (value == wInfo)
                    return;

                wInfo = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        public object MainSelected
        {
            get { return wMainSelected; }

            set
            {
                if (value == wMainSelected)
                    return;

                wMainSelected = value;
                OnPropertyChanged(nameof(MainSelected));
            }
        }

        public object SecondarySelected
        {
            get { return wSecondarySelected; }

            set
            {
                if (value == wSecondarySelected)
                    return;

                wSecondarySelected = value;
                OnPropertyChanged(nameof(SecondarySelected));
            }
        }

        public object ControlTag
        {
            get { return wControlTag; }

            set
            {
                if (value == wControlTag)
                    return;

                wControlTag = value;
                OnPropertyChanged(nameof(ControlTag));
            }
        }

        public object MainTag
        {
            get { return wMainTag; }

            set
            {
                if (value == wMainTag)
                    return;

                wMainTag = value;
                OnPropertyChanged(nameof(MainTag));
            }
        }

        public object SecondaryTag
        {
            get { return wSecondaryTag; }

            set
            {
                if (value == wSecondaryTag)
                    return;

                wSecondaryTag = value;
                OnPropertyChanged(nameof(SecondaryTag));
            }
        }

        #endregion
    }
}