using System;

namespace Sensors
{
    public class HumiditySensor
    {
        public float GetHumidity()
        {
            Random r = new Random();
            float humidity = r.Next(0, 100);
            return humidity;
        }
    }
}
