using PeopleViewApp.Stores;
using PeopleViewApp.ViewModels;

namespace PeopleViewApp.Commands
{
    public class NavigateCommand<TVievModel> : CommandBase where TVievModel : ViewModelBase
    {
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
            : base(canExecuteFunc)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
