using System;

namespace Sensors
{
    public class TemperatureSensor
    {
        public float GetTemperature()
        {
            Random r = new Random();
            float temperature = r.Next(-20, 39);
            return temperature;
        }
    }
}
