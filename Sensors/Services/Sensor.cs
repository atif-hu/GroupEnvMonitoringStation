namespace Sensors.Services
{
    public class Sensor
    {
        private Random random = new Random();

        protected double GenerateData(double min, double max)
        {
            return Math.Round(random.NextDouble() * (max - min) + min, 2);
        }
    }
}
