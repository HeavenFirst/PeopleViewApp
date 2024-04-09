using PeopleViewApp.Stores;
using PeopleViewApp.ViewModels;

namespace PeopleViewApp.Commands
{
    public class NavigateCommand<TVievModel> : CommandBase where TVievModel : ViewModelBase
    {
        public readonly NavigationStore _navigationStore;
        private readonly Func<TVievModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TVievModel> createViewModel)
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
