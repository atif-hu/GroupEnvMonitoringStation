using Microsoft.EntityFrameworkCore;
using MonitoringStationAPI.Models;

namespace MonitoringStationAPI.Database
{
    public class EnvironmentDbContext : DbContext
    {
        public EnvironmentDbContext(DbContextOptions<EnvironmentDbContext> options) : base(options)
        {
        }

        public DbSet<EnvironmentSensor> EnvironmentSensors { get; set; } = null!;
    }
}
