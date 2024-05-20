using System.ComponentModel.DataAnnotations;

namespace MonitoringStationAPI.Models
{
    public class CO2EmissionsData
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public float CO2Emissions { get; set; }
        public bool TriggerThresholdWarning { get; set; }

    }
}
