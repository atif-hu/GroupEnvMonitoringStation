using backend.Models;
using backend.Database;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly GroupMonitoringStationDbContext _dbContext;

        public UserService(GroupMonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users Authenticate(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == username && x.Password == password);

            if (user == null)
                return null;

            user.Password = null;

            return user;
        }

        public bool UserExists(string username, string email)
        {
            return _dbContext.Users.Any(x => x.Email == username || x.Email == email);
        }

        public void AddUser(Users user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
