// DatabaseContextSingleton.cs
using Microsoft.EntityFrameworkCore;
using MonitoringStationAPI.Models;

namespace MonitoringStationAPI.Database
{
    public class DatabaseContextSingleton
    {
        private static DatabaseContextSingleton? _instance;
        private readonly MonitoringStationDbContext _dbContext;

        private DatabaseContextSingleton()
        {
            // Initialize database context
            var options = new DbContextOptionsBuilder<MonitoringStationDbContext>()
                .UseSqlite("SQLiteConnection")
                .Options;
            _dbContext = new MonitoringStationDbContext(options);
        }

        public static DatabaseContextSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseContextSingleton();
                }
                return _instance;
            }
        }

        public MonitoringStationDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
