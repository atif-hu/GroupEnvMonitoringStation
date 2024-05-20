using MonitoringStationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MonitoringStationAPI.Database
{
    public class MonitoringStationDbContext : DbContext
    {
        public MonitoringStationDbContext(DbContextOptions<MonitoringStationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<TemperatureData> TemperatureData { get; set; }
        public DbSet<RainfallData> RainfallData { get; set; }
        public DbSet<HumidityData> HumidityData { get; set; }
        public DbSet<CO2EmissionsData> CO2EmissionsData{ get; set; }
        public DbSet<AirPollutionData> AirPollutionData{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=MonitoringStation.db");
    }
}