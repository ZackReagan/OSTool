namespace OSTool.Core
{
    public abstract class DialogViewModelBase<T>
    {
        public int DialogCornerRadius => 25;

        public object Caption { get; set; }

        public object Info { get; set; }

        public T DialogResult { get; set; }

        public DialogViewModelBase() : this(null, string.Empty) { }
        public DialogViewModelBase(string title) : this(title, string.Empty) { }
        public DialogViewModelBase(string title, string message)
        {
            Caption = title;
            Info = message;
        }

        public void CloseDialog(IDialogWindow dialog, T result)
        {
            DialogResult = result;
            if (dialog != null) { dialog.DialogResult = true; }
        }
    }
}