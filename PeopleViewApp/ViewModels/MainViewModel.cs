using PeopleViewApp.Stores;
using System.Windows.Input;

namespace PeopleViewApp.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanget;
        }

        private void OnCurrentViewModelChanget()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        //public ICommand CloseCommand { get; }

        //public ICommand MinimizeCommand { get; }

        //public ICommand SkipCommand { get; }

        //public DialogViewModel Dialog { get; }

    }
}
