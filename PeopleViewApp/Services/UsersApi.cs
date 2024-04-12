using Microsoft.Extensions.Options;
using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Settings;
using System.Net.Http;
using System.Security.Policy;

namespace PeopleViewApp.Services
{
    public class UsersApi : BaseApi, IUsersApi
    {
        public UsersApi(IHttpClientFactory httpClientFactory, IOptions<AppSettings> settings)
            : base(httpClientFactory, settings)
        {
        }

        public async Task DeleteUser(long id)
        {
            await HttpRequestRun<User>(HttpMethod.Delete, $"DeleteUser?id={id}");
        }

        public async Task<User> CreateUser(User user)
        {
            return await HttpRequestRun<User>( HttpMethod.Post, "CreateUser", user);
        }

        public async Task<User> EditUser(User user)
        {
            return await HttpRequestRun<User>(HttpMethod.Put, "EditUser", user);
        }

        public async Task<User> GetUser(long id)
        {
            return await HttpRequestRun<User>(HttpMethod.Get, $"GetUser?id=/{id}");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await HttpRequestRun<List<User>>(HttpMethod.Get, $"GetUsers");
        }

    }
}
