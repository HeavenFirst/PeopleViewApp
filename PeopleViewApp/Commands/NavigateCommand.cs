using PeopleViewApp.Stores;
using PeopleViewApp.ViewModels;
using System.Windows.Input;

namespace PeopleViewApp.Commands
{
    public class NavigateCommand<TVievModel> : ICommand where TVievModel : ViewModelBase
    {
        private readonly Func<object, bool> _canExecuteFunc;
        public readonly NavigationStore _navigationStore;
        private readonly Func<TVievModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore,
            Func<TVievModel> createViewModel,
            Func<bool> canExecuteFunc)
            : this(
                  navigationStore = navigationStore,
                  createViewModel = createViewModel,
                  canExecuteFunc == null ? null : (Func<object, bool>)(o => canExecuteFunc()))
        {
        }

        public NavigateCommand(NavigationStore navigationStore,
            Func<TVievModel> createViewModel, 
            Func<object, bool> canExecuteFunc)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
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

        public virtual void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }

        public void RaiseCanExecuteChanged()
        {
            if (_canExecuteFunc != null)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
