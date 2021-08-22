namespace OSTool.Core
{
    public class ExpanderViewModel : BaseViewModel
    {
        #region Private

        private object wCaption;

        private object wInfo;

        private object wControlTag;

        private object wMainContent;

        private object wMainSelected = "False";

        private object wMainTag;

        private object wSecondaryVisibility;

        private object wSecondaryContent;

        private object wSecondarySelected = "False";

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

        public object MainContent
        {
            get { return wMainContent; }

            set
            {
                if (value == wMainContent)
                    return;

                wMainContent = value;
                OnPropertyChanged(nameof(MainContent));
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

        public object SecondaryContent
        {
            get { return wSecondaryContent; }

            set
            {
                if (value == wSecondaryContent)
                    return;

                wSecondaryContent = value;
                OnPropertyChanged(nameof(SecondaryContent));
            }
        }

        public object SecondaryVisibility
        {
            get { return wSecondaryVisibility; }

            set
            {
                if (value == wSecondaryVisibility)
                    return;

                wSecondaryVisibility = value;
                OnPropertyChanged(nameof(SecondaryVisibility));
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