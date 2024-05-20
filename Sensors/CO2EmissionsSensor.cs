using System;

namespace Sensors
{
    public class CO2EmissionsSensor
    {

        public float GetCO2Emissions()
        {
            Random r = new Random();
            float CO2 = r.Next(1, 100);
            return CO2;
        }
    }
}
