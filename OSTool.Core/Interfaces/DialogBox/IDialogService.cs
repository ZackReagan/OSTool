namespace OSTool.Core
{
    public interface IDialogService
    {
        T ShowDialog<T>(DialogViewModelBase<T> model);
    }
}