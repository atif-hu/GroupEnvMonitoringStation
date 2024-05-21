using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Repository.Service
{
    public class AirPollutionService : IAirPollution
    {
        public double GetAirPollution()
        {
            double AirPollution = 0;
            AirPollutionSensor AirPollutionObj = new AirPollutionSensor();
            if (AirPollutionObj != null)
            {
                AirPollution = AirPollutionObj.GenerateAirPollution();
            }
            return AirPollution;
        }
    }
}
