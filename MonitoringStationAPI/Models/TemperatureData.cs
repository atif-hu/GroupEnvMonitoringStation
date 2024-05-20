using System.ComponentModel.DataAnnotations;

namespace MonitoringStationAPI.Models
{
    public class TemperatureData
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public float Temperature { get; set; }
        public bool TriggerThresholdWarning { get; set; }
    }
}
