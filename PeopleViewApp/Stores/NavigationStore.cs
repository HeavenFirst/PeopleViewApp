using PeopleViewApp.ViewModels;

namespace PeopleViewApp.Stores
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanget();
            }
        }

        private void OnCurrentViewModelChanget()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
