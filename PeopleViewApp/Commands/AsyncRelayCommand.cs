using System.Windows.Input;

namespace PeopleViewApp.Commands
{
    internal class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, Task> _asyncExecuteAction;
        private readonly Func<object, bool> _canExecuteFunc;
        private bool _isExecutingNow;

        public AsyncRelayCommand(Func<Task> asyncExecuteAction, Func<bool> canExecuteFunc = null)
            : this(
                asyncExecuteAction == null ? null : (Func<object, Task>)(o => asyncExecuteAction()),
                canExecuteFunc == null ? null : (Func<object, bool>)(o => canExecuteFunc()))
        {
        }

        protected AsyncRelayCommand(Func<object, Task> asyncExecuteAction, Func<object, bool> canExecuteFunc = null)
        {
            _asyncExecuteAction = asyncExecuteAction ?? throw new ArgumentNullException(nameof(asyncExecuteAction));
            _canExecuteFunc = canExecuteFunc;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();

        public bool CanExecute(object parameter) => !_isExecutingNow && (_canExecuteFunc == null || _canExecuteFunc.Invoke(parameter));

        public virtual async void Execute(object parameter)
        {
            try
            {
                _isExecutingNow = true;
                await _asyncExecuteAction(parameter).ConfigureAwait(true);
            }
            finally
            {
                _isExecutingNow = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}
