using System;
using System.ComponentModel.DataAnnotations;

namespace MonitoringStationAPI.Models
{
    public class EnvironmentSensor
    {
        [Key]
        public int Id { get; set; }
        public string? EnvironmentParameter { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
