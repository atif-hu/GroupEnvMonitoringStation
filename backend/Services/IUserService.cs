using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
        bool UserExists(string username, string email);
        void AddUser(Users user);
    }
}
