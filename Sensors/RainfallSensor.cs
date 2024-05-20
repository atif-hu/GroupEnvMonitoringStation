using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    public class RainfallSensor
    {
        public float GetRainfall()
        {
            Random r = new Random();
            float rainfall = r.Next(0, 40);
            return rainfall;
        }
    }
}
