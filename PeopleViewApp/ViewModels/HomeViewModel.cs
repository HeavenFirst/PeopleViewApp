using PeopleViewApp.Commands;
using PeopleViewApp.Models;
using PeopleViewApp.Services;
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
        private static User _user;
        public string Name => "Home Page";

        public ICommand NavigateUserPageCommand { get; }
        public ICommand DeleteSelectedRowCommand { get; }
        public ICommand NewUserCreateCommand { get; }

        public HomeViewModel(NavigationStore navigationStore, IUsersApi usersApi)
        {
            _usersApi = usersApi;

            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _user, _usersApi));

            DeleteSelectedRowCommand = new RelayCommand(() => DeleteRow(), () => true);

            NewUserCreateCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _usersApi));

            Task.Run(() => GetUsers()).Wait();
        }

        public HomeViewModel(NavigationStore navigationStore, IUsersApi usersApi, User user, bool? IsCreating)
        {
            _usersApi = usersApi;
            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore, 
                () => new UsersPageViewModel(navigationStore, _user, _usersApi));

            DeleteSelectedRowCommand = new RelayCommand(() => DeleteRow(), () => true);

            NewUserCreateCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _usersApi));
           
            if (_users.Count > 0)
            {
                if (IsCreating == true)
                {
                    Task.Run(() => CreateUserFromDb(user)).Wait();
                    _users.Add(user);
                }
                else if (IsCreating == false)
                {
                    Task.Run(() => EditUserFromDb(_user)).Wait();                    
                }
            }
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

        private void EditUser(User user)
        {
            var u = _users.FirstOrDefault(x => x.Id == user.Id);
            u = user;
        }

        private void DeleteRow()
        {
            Task.Run(() => DeleteUser()).Wait();
            Users.Remove(User);
        }

        private async Task DeleteUser()
        {
            await _usersApi.DeleteUser(_user.Id);
        }

        private async Task GetUsers()
        {
            var users = await _usersApi.GetUsers();
            _users = new ObservableCollection<User>(users);
        }
        private async Task CreateUserFromDb(User user)
        {
            await _usersApi.CreateUser(user);
        }

        private async Task EditUserFromDb(User user)
        {
            var editedUser = await _usersApi.EditUser(user);
            EditUser(editedUser);
        }
    }
}
