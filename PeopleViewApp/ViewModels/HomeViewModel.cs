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
        private static User _user;

        public ICommand NavigateUserPageCommand { get; }
        public ICommand DeleteSelectedRowCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand RefreshCommand { get; }

        public HomeViewModel(NavigationStore navigationStore, IUsersApi usersApi, User user = null, bool? IsCreating = null)
        {
            _usersApi = usersApi;

            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _user, _usersApi), IsUserSelected);

            DeleteSelectedRowCommand = new RelayCommand(() => DeleteRow(), IsUserSelected);

            CreateUserCommand = new NavigateCommand<UsersPageViewModel>(navigationStore,
                () => new UsersPageViewModel(navigationStore, _usersApi), () => true);

            RefreshCommand = new RelayCommand(() => GetUserAsync(), () => true);

            UserChecking(user, IsCreating);
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

        private bool IsUserSelected()
        {
            return User != null;
        }

        private void UserChecking(User user, bool? IsCreating)
        {
            if (user is not null)
            {
                if (IsCreating == true)
                {
                    Task.Run(() => SaveUser(user)).Wait();
                    _users.Add(user);
                }
                else if (IsCreating == false)
                {
                    Task.Run(() => SaveUsersChanges(_user)).Wait();
                }
            }
            else
            {
                GetUserAsync();
            }
        }

        private static void EditUser(User user) =>
            _users.FirstOrDefault(x => x.Id == user.Id);

        private void DeleteRow()
        {
            Task.Run(() => DeleteUser()).Wait();
            Users.Remove(User);
        }

        private async Task DeleteUser() =>
            await _usersApi.DeleteUser(_user.Id);

        private void GetUserAsync()
        {
            Task.Run(() => GetUsers()).Wait();
        }

        private async Task GetUsers()
        {
            var users = await _usersApi.GetUsers();
            _users = new ObservableCollection<User>(users);
        }
        private async Task SaveUser(User user) =>
            await _usersApi.CreateUser(user);

        private async Task SaveUsersChanges(User user)
        {
            var editedUser = await _usersApi.EditUser(user);
            EditUser(editedUser);
        }
    }
}
