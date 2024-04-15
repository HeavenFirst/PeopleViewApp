using System.Windows.Input;

namespace PeopleViewApp.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecuteFunc;
        private readonly Action<object> _executeAction;

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc = null)
            : this(
                executeAction == null ? null : (Action<object>)(o => executeAction()),
                canExecuteFunc == null ? null : (Func<object, bool>)(o => canExecuteFunc()))
        {
        }

        protected RelayCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc = null)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
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

        public virtual void Execute(object parameter) => _executeAction(parameter);

        public void RaiseCanExecuteChanged()
        {
            if (_canExecuteFunc != null)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}

