using PeopleViewApp.Commands;
using PeopleViewApp.Models;
using PeopleViewApp.Stores;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PeopleViewApp.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        private User _user;

        public ICommand SaveCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        public UsersPageViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore,
                () => new HomeViewModel(navigationStore));

            _user = new User();
            //SaveCommand = new RelayCommand(SaveData);
        }

        public string FirstName
        {
            get => _user.FirstName;
            set
            {
                _user.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _user.LastName;
            set
            {
                _user.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string StreetName
        {
            get => _user.StreetName;
            set
            {
                _user.StreetName = value;
                OnPropertyChanged(nameof(StreetName));
            }
        }

        public string HouseNumber
        {
            get => _user.HouseNumber;
            set
            {
                _user.HouseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        public string ApartmentNumber
        {
            get => _user.ApartmentNumber;
            set
            {
                _user.ApartmentNumber = value;
                OnPropertyChanged(nameof(ApartmentNumber));
            }
        }

        public string PostalCode
        {
            get => _user.PostalCode;
            set
            {
                _user.PostalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        public string Town
        {
            get => _user.Town;
            set
            {
                _user.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        public string PhoneNumber
        {
            get => _user.PhoneNumber;
            set
            {
                _user.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime DateOfBirth
        {
            get => _user.DateOfBirth == DateTime.MinValue ? DateTime.UtcNow : _user.DateOfBirth;
            set
            {
                _user.DateOfBirth = value;
                Age = value.ToString();
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Age
        {
            get => CalculateAge();
            set
            {
                _user.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string CalculateAge()
        {
            if (DateOfBirth.Year == DateTime.UtcNow.Year)
            {
                return string.Empty;
            }

            var today = DateTime.Today;

            var age = today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > today.AddYears(-age)) age--;

            return age.ToString();
        }

        private void SaveData()
        {
            MessageBox.Show($"data of {FirstName} {LastName} are saved!");
            LastName = null;
            FirstName = null;
            _user = new User();
        }
    }
}
