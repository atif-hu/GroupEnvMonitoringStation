namespace MonitoringStationAPI.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string ?ParameterType { get; set; }
        public string ?DataCollectionInterval { get; set; }
        public float DataRangeMin { get; set; }
        public float DataRangeMax { get; set; }
        public string ?Unit { get; set; }
        public float ?NormalThresholdMin { get; set; }
        public float ?NormalThresholdMax { get; set; }
    }
}
