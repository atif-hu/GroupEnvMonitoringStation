namespace Sensors.Services
{
    public class CO2EmissionsSensor : Sensor
    {
        int Number_of_CO2EmissionSensors = 5;
        int MinCO2Emission = 1;
        int MaxCO2Emission = 100;
        public double GenerateCO2Emission()
        {
            return GenerateData(MinCO2Emission, MaxCO2Emission);
        }
    }
}
