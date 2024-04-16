using System.Windows.Input;

namespace PeopleViewApp.Commands
{
    public abstract class CommandBase : ICommand
    {
        private readonly Func<object, bool> _canExecuteFunc;

        public CommandBase(Action executeAction, Func<bool> canExecuteFunc = null)
            : this(
                canExecuteFunc == null ? null : (Func<object, bool>)(o => canExecuteFunc()))
        {
        }

        protected CommandBase(Func<object, bool> canExecuteFunc = null)
        {
            _canExecuteFunc = canExecuteFunc;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecuteFunc != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecuteFunc != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter) => _canExecuteFunc?.Invoke(parameter) ?? true;

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            if (_canExecuteFunc != null)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
