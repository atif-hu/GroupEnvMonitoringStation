using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Repository.Service
{
    public class HumidityService : IHumidity
    {
        public double GetHumidity()
        {
            double Humidity = 0;
            HumiditySensor HumidityObj = new HumiditySensor();
            if (HumidityObj != null)
            {
                Humidity = HumidityObj.GenerateHumidity();
            }
            return Humidity;
        }
    }
}
