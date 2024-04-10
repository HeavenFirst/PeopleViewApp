using Microsoft.Extensions.Options;
using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Settings;
using System.Net.Http;

namespace PeopleViewApp.Services
{
    public class UsersApi : BaseApi, IUsersApi
    {
        public UsersApi(IHttpClientFactory httpClientFactory, IOptions<AppSettings> settings)
            : base(httpClientFactory, settings)
        {
        }

        public async Task DeleteUser(int id)
        {
            await HttpRequestRun<User>(HttpMethod.Put, $"deleteuser/{id}");
        }

        public async Task<User> CreateUser(User user)
        {
            return await HttpRequestRun<User>( HttpMethod.Put, "createuser", user);
        }

        public async Task<User> EditUser(User user)
        {
            return await HttpRequestRun<User>(HttpMethod.Post, "edituser", user);
        }

        public async Task<User> GetUser(int id)
        {
            return await HttpRequestRun<User>(HttpMethod.Get, $"getuser/{id}");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return new List<User>()
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
           // return await HttpRequestRun<List<User>>(HttpMethod.Get, $"getusers");
        }

    }
}
