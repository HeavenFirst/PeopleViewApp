using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Settings;
using System.Net.Http;
using System.Text.Json;

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
            return await HttpRequestRun<List<User>>(HttpMethod.Get, $"getusers");
        }

    }
}
