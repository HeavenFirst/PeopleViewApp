using PeopleViewApp.Models;

namespace PeopleViewApp.Services.Interfaces
{
    public interface IUsersApi
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> EditUser(User user);
        Task DeleteUser(int id);

    }
}
