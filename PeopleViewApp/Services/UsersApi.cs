using PeopleViewApp.Models;
using PeopleViewApp.Services.Interfaces;

namespace PeopleViewApp.Services
{
    public class UsersApi : IUsersApi
    {
        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
