using System;
using System.Windows.Input;

namespace OSTool.Core
{
    public class BaseCommand : ICommand
    {
        private Action<object> mExecute;
        private Func<object, bool> mCanExecute;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public BaseCommand(Action<object> execute, Func<object, bool> canexecute = null)
        {
            mExecute = execute;
            mCanExecute = canexecute;
        }

        public bool CanExecute(object parameter)
        {
            return mCanExecute == null || mCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            mExecute(parameter);
        }
    }
}