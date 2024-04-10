using PeopleViewApp.Commands;
using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PeopleViewApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IUsersApi _usersApi;

        private static ObservableCollection<User> _users = [];
        private User _user;
        public string Name => "Home Page";

        public ICommand NavigateUserPageCommand { get; }
        public ICommand DeleteSelectedRowCommand { get; }
        public ICommand NewUserCreateCommand { get; }

        public HomeViewModel(NavigationStore navigationStore, 
            IUsersApi usersApi)
        {
            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _user));

            DeleteSelectedRowCommand = new RelayCommand(() => DeleteRow(), () => true);

            NewUserCreateCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore));

            _usersApi = usersApi;

            _users = new ObservableCollection<User>(_usersApi.GetUsers().Result);            
        }

        public HomeViewModel(NavigationStore navigationStore, User user, bool? IsCreating)
        {
            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore, 
                () => new UsersPageViewModel(navigationStore, _user));

            DeleteSelectedRowCommand = new RelayCommand(() => DeleteRow(), () => true);

            NewUserCreateCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore));
           
            if (_users.Count > 0)
            {
                if (IsCreating == true)
                {
                    _users.Add(user);
                }
                else if (IsCreating == false)
                {
                    EditUser(user);
                }
            }
        }

        private void EditUser(User user)
        {
            var u = _users.FirstOrDefault(x => x.Id == user.Id);
            u = user;
        }

        private void DeleteRow()
        {
            Users.Remove(User);
        }

        public ObservableCollection<User> Users 
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
    }
}
