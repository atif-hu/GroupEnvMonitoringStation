using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sensors.Services;
using SensorsAPI.Repository.Interface;

namespace SensorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private ITemperature _temperature;
        private IRainfall _rainfall;
        private IHumidity _humidity;
        private ICO2Emission _co2emission;
        private IAirPollution _airPollution;

        public SensorController(ITemperature temperature,IRainfall rainfall,IHumidity Humidity,ICO2Emission CO2Emission,IAirPollution AirPollution)
        { 
            _temperature = temperature;
            _rainfall = rainfall;
            _humidity = Humidity;
            _co2emission = CO2Emission;
            _airPollution = AirPollution;
        }
        [HttpGet("GetTemp")]
        public IActionResult GetTempFromSensor()
        {
            var temp = _temperature.GetTemperature();
            return Ok(new { Temprature = temp });
        }

        [HttpGet("GetRainFall")]
        
        public IActionResult GetRainFromSensor()
        {
            var rain = _rainfall.GetRainFall();
            return Ok(new { Rainfall = rain });
        }

        [HttpGet("GetHumidity")]

        public IActionResult GetHumidityFromSensor()
        {
            var humid = _humidity.GetHumidity();
            return Ok(new { Humidity = humid });
        }

        [HttpGet("GetCO2Emission")]
        public IActionResult GetCO2EmissionFromSensor()
        {
            var co2 = _co2emission.GetCO2Emission();
            return Ok(new { CO2Emission = co2 });
        }

        [HttpGet("GetAirPollution")]

        public IActionResult GetAirPollutionFromSensor()
        {
            var airp = _airPollution.GetAirPollution();
            return Ok(new { AirPollution = airp });
        }
    }

}
