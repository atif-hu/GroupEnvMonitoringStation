namespace Sensors.Services
{
    public class HumiditySensor : Sensor
    {
        int Number_of_Humidity_Sensors = 1;
        int MinimumHumidity = 0;
        int MaximumHumidity = 100;
        public double GenerateHumidity()
        {
            return GenerateData(MinimumHumidity, MaximumHumidity);
        }
    }
}
