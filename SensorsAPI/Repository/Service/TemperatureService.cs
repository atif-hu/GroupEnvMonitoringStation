using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Repository.Service
{
    public class TemperatureService : ITemperature
    {
        public double GetTemperature()
        {
            double temperature = 0;
            TemperatureSensor temperatureObj = new TemperatureSensor();
            if (temperatureObj != null)
            {
                temperature = temperatureObj.GenerateTemperature();
            }
            return temperature;
        }
    }
}
