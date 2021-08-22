using OSTool.Core;

namespace OSTool
{
    public class DialogService : IDialogService
    {
        public T ShowDialog<T>(DialogViewModelBase<T> model)
        {
            IDialogWindow window = new DialogBox();
            window.DataContext = model;
            window.ShowDialog();

            return model.DialogResult;
        }
    }
}