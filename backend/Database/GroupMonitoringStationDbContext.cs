using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Database
{
    public class GroupMonitoringStationDbContext : DbContext
    {
        public GroupMonitoringStationDbContext(DbContextOptions<GroupMonitoringStationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=GroupMonitoringStation.db");
            }
        }
    }
}
