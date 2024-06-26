﻿using PeopleViewApp.Commands;
using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Stores;
using System.Windows.Input;

namespace PeopleViewApp.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        private User _user;

        public ICommand NavigateHomeCommand { get; }

        public UsersPageViewModel(NavigationStore navigationStore, IUsersApi usersApi)
        {
            _user = new User();

            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore,
                () => new HomeViewModel(navigationStore, usersApi, _user, true), FieldsChecking);
        }

        public UsersPageViewModel(NavigationStore navigationStore, User user, IUsersApi usersApi)
        {
            _user = user;

            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore,
                () => new HomeViewModel(navigationStore, usersApi, _user, false), FieldsChecking);
        }

        public string FirstName
        {
            get => _user.FirstName;
            set
            {
                _user.FirstName = value?.Trim();
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _user.LastName;
            set
            {
                _user.LastName = value?.Trim();
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string StreetName
        {
            get => _user.StreetName;
            set
            {
                _user.StreetName = value?.Trim();
                OnPropertyChanged(nameof(StreetName));
            }
        }

        public string HouseNumber
        {
            get => _user.HouseNumber;
            set
            {
                _user.HouseNumber = value?.Trim();
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        public string ApartmentNumber
        {
            get => _user.ApartmentNumber;
            set
            {
                _user.ApartmentNumber = value?.Trim();
                OnPropertyChanged(nameof(ApartmentNumber));
            }
        }

        public string PostalCode
        {
            get => _user.PostalCode;
            set
            {
                _user.PostalCode = value?.Trim();
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        public string Town
        {
            get => _user.Town;
            set
            {
                _user.Town = value?.Trim();
                OnPropertyChanged(nameof(Town));
            }
        }

        public string PhoneNumber
        {
            get => _user.PhoneNumber;
            set
            {
                _user.PhoneNumber = value?.Trim();
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime DateOfBirth
        {
            get => _user.DateOfBirth == DateTime.MinValue ? DateTime.UtcNow : _user.DateOfBirth;
            set
            {
                _user.DateOfBirth = value;
                Age = CalculateAge();
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Age
        {
            get => _user.Age;
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

            int age = today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > today.AddYears(-age)) age--;

            return age.ToString();
        }

        private bool FieldsChecking() =>
            FirstNameChecking()
                && LastNameChecking()
                && StreetNameChecking()
                && HouseNumberChecking()
                && ApartmentNumberChecking()
                && PostalCodeChecking()
                && TownChecking()
                && PhoneNumberCheching()
                /*&& DateOfBirthChecking()*/;

        private bool StreetNameChecking() =>
            !string.IsNullOrWhiteSpace(StreetName);

        private bool DateOfBirthChecking() =>
            DateOfBirth.Year < DateTime.UtcNow.Year && DateOfBirth != DateTime.MinValue;

        private bool PhoneNumberCheching() =>
            !string.IsNullOrWhiteSpace(PhoneNumber)
                && !PhoneNumber.ToCharArray().Any(x => !(char.IsDigit(x) || char.IsWhiteSpace(x) || char.Equals(x, '+')));

        private bool TownChecking() => !string.IsNullOrWhiteSpace(Town);

        private bool ApartmentNumberChecking() =>
            (ApartmentNumber?.ToCharArray().Any(x => !char.IsDigit(x))) != true;

        private bool LastNameChecking() => !string.IsNullOrWhiteSpace(LastName);

        private bool FirstNameChecking() => !string.IsNullOrWhiteSpace(FirstName);

        private bool HouseNumberChecking() =>
            !string.IsNullOrWhiteSpace(HouseNumber)
                && !HouseNumber.ToCharArray().Any(x => !(char.IsDigit(x) || char.IsWhiteSpace(x) || char.IsLetter(x)));

        private bool PostalCodeChecking() =>
            !string.IsNullOrWhiteSpace(PostalCode)
                && !PostalCode.ToCharArray().Any(x => !(char.IsDigit(x)
                    || char.IsWhiteSpace(x)
                    || char.IsLetter(x)
                    || char.Equals(x, '-')));
    }
}
