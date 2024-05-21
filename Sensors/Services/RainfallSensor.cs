namespace Sensors.Services
{
    public class RainfallSensor : Sensor
    {
        int Number_of_Rainfall_Sensors = 2;
        int Minimum_Rainfall_Limit = 0;
        int Maximum_Rainfall_Limit = 40;

        public double GenerateRainfall()
        {
            return GenerateData(Minimum_Rainfall_Limit, Maximum_Rainfall_Limit);
        }
    }
}
