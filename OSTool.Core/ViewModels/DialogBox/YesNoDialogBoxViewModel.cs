namespace OSTool.Core
{
    public class YesNoDialogBoxViewModel : DialogViewModelBase<DialogBoxResult>
    {
        public BaseCommand YesCommand { get; set; }

        public BaseCommand NoCommand { get; set; }

        public YesNoDialogBoxViewModel(string title, string message) : base(title, message)
        {
            YesCommand = new BaseCommand(e => YesResult(e as IDialogWindow));
            NoCommand = new BaseCommand(e => NoResult(e as IDialogWindow));
        }

        private void YesResult(IDialogWindow window)
        {
            CloseDialog(window, DialogBoxResult.Yes);
        }

        private void NoResult(IDialogWindow window)
        {
            CloseDialog(window, DialogBoxResult.No);
        }
    }
}