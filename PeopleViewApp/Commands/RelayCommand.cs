namespace PeopleViewApp.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action<object> _executeAction;

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc = null)
            : this(
                executeAction == null ? null : (Action<object>)(o => executeAction()),
                canExecuteFunc == null ? null : (Func<object, bool>)(o => canExecuteFunc()))
        {
        }

        protected RelayCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc = null)
            : base(canExecuteFunc)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
        }

        public override void Execute(object parameter) => _executeAction(parameter);
    }
}

