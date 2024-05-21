using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Repository.Service
{
    public class RainfallService : IRainfall
    {
        public double GetRainFall()
        {
            double Rainfall = 0;
            RainfallSensor RainfallObj = new RainfallSensor();
            if (RainfallObj != null)
            {
                Rainfall = RainfallObj.GenerateRainfall();
            }
            return Rainfall;
        }
    }
}
