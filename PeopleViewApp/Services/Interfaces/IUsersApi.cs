using PeopleViewApp.Models;

namespace PeopleViewApp.Services.Interfaces
{
    public interface IUsersApi
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(long id);
        Task<User> EditUser(User user);
        Task DeleteUser(long id);
        Task<User> CreateUser(User user);

    }
}
