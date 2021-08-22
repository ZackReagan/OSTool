namespace OSTool.Core
{
    public class Dialog
    {
        public static DialogBoxResult Show(string title, string message = null, DialogBoxType type = DialogBoxType.OK)
        {
            return Result(title, message, type);
        }

        public static DialogBoxResult Show(string title, DialogBoxType type)
        {
            return Result(title, string.Empty, type);
        }

        private static DialogBoxResult Result(string title, string message = null, DialogBoxType type = DialogBoxType.OK)
        {
            if (type == DialogBoxType.OK)
            {
                return IoC.Dialog.ShowDialog(new OKDialogBoxViewModel(title, message));
            }
            else
            {
                return IoC.Dialog.ShowDialog(new YesNoDialogBoxViewModel(title, message));
            }
        }
    }
}