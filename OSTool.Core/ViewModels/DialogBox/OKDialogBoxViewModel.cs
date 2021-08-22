namespace OSTool.Core
{
    public class OKDialogBoxViewModel : DialogViewModelBase<DialogBoxResult>
    {
        public BaseCommand OKCommand { get; set; }

        public OKDialogBoxViewModel(string title, string message) : base(title, message)
        {
            OKCommand = new BaseCommand(e => OKResult(e as IDialogWindow));
        }

        private void OKResult(IDialogWindow window)
        {
            CloseDialog(window, DialogBoxResult.None);
        }
    }
}