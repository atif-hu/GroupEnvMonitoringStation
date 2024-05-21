
namespace Sensors.Services
{
    public class AirPollutionSensor : Sensor
    {
        int Number_Of_Airpollution_Sensors = 1;
        int MinAirPollution = 1;
        int MaxAirPollution = 10;
        public double GenerateAirPollution()
        {
            return GenerateData(MinAirPollution, MaxAirPollution);
        }

        public object? GenerateAirPolution()
        {
            throw new NotImplementedException();
        }
    }
}
