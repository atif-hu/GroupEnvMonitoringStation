using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Repository.Service
{
    public class CO2EmissionService : ICO2Emission
    {
        public double GetCO2Emission()
        {
            double CO2Emission = 0;
            CO2EmissionsSensor CO2EmissionObj = new CO2EmissionsSensor();
            if (CO2EmissionObj != null)
            {
                CO2Emission = CO2EmissionObj.GenerateCO2Emission();
            }
            return CO2Emission;
        }
    }
}
