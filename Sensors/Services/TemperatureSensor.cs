namespace Sensors.Services
{
    public class TemperatureSensor  : Sensor
    {
        int Number_of_Temperature_Sensors = 3;
        int MinTempLimit = -20;
        int MaxTempLimit = 39;
        public double GenerateTemperature()
        {
            return GenerateData(MinTempLimit, MaxTempLimit);
        }
    }
}
