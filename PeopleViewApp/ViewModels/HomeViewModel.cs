using PeopleViewApp.Commands;
using PeopleViewApp.Models;
using PeopleViewApp.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PeopleViewApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<User> _users = [];
        private User _user;
        public string Name => "Home Page";

        public ICommand NavigateUserPageCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateUserPageCommand = new NavigateCommand<UsersPageViewModel>(navigationStore, 
                () => new UsersPageViewModel(navigationStore));

            _users = new ObservableCollection<User>
            {
                new User{FirstName = "Dima", LastName = "Shostak", DateOfBirth = DateTime.Now.AddYears(-40), StreetName = "Daslovicha", ApartmentNumber = "2",
                        HouseNumber = "3", Town = "New York", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" },
                new User{FirstName = "Dasha", LastName = "Shostak", DateOfBirth = DateTime.Now.AddYears(-33), StreetName = "Perslovicha", ApartmentNumber = "2",
                        HouseNumber = "3",Town = "New York", Id = Guid.NewGuid().ToString(), PhoneNumber = "+5234567890", PostalCode = "123-1234" },
                new User{FirstName = "D", LastName = "Sho", DateOfBirth = DateTime.Now.AddYears(-45), StreetName = "Wraslovicha", ApartmentNumber = "7",
                        HouseNumber = "3",Town = "Boston", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" },
                new User{FirstName = "Dima", LastName = "ASDFT", DateOfBirth = DateTime.Now.AddYears(-40), StreetName = "Wrovicha", ApartmentNumber = "8",
                        HouseNumber = "3",Town = "New York", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" },
                new User{FirstName = "Lesha", LastName = "Buss", DateOfBirth = DateTime.Now.AddYears(-18), StreetName = "Fvicha", ApartmentNumber = "2",
                        HouseNumber = "3",Town = "Chikago", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" },
                new User{FirstName = "Gosha", LastName = "Koi", DateOfBirth = DateTime.Now.AddYears(-25), StreetName = "Grepovicha", ApartmentNumber = "1",
                        HouseNumber = "3",Town = "Madrid", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" },
                new User{FirstName = "Igor", LastName = "Ger", DateOfBirth = DateTime.Now.AddYears(-13), StreetName = "Bovicha", ApartmentNumber = "9",
                        HouseNumber = "3",Town = "London", Id = Guid.NewGuid().ToString(), PhoneNumber = "+1234567890", PostalCode = "123-1234" }
            };
            //UsersListBox.ItemsSource = _user;
        }

        public ObservableCollection<User> Users 
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
    }
}
