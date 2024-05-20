using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;

namespace SensorsAPI.Controllers
{
    [Route("api/air-pollution-sensor")]
    [ApiController]
    public class AirPollutionSensorController : ControllerBase // Changed from Controller to ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AirPollutionSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<float>> GetAirPollution(int sensorId)
        {
            var airPollutionSensor = new AirPollutionSensor(); // Create instance of AirPollutionSensor
            float airPollutionValue = airPollutionSensor.GetAirPollution();

            bool triggerWarning = false;       

            if (airPollutionValue < 1 || airPollutionValue > 9)
            {
                triggerWarning = true;
                Console.WriteLine($"Warning: Air Pollution levels have crossed the normal threshold with {airPollutionValue} index. Immediate attention required.");
            }

            TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

            var airPollutionData = new AirPollutionData
            {
                SensorId = sensorId,
                Timestamp = ukTime,
                AirPollution = airPollutionValue,
                TriggerThresholdWarning = triggerWarning
            };

            // Serialize temperature data to JSON
            var jsonContent = JsonSerializer.Serialize(airPollutionData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to Monitoring Station API
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7051/"); // Replace with Monitoring Station API base URL

                var response = await httpClient.PostAsync("/api/air-pollution-monitoring", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(airPollutionData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to send air pollution data to Monitoring Station API.");
                }
            }

        }

        public class AirPollutionData
        {
            public int Id { get; set; }
            public int SensorId { get; set; }
            public DateTime Timestamp { get; set; }
            public float AirPollution { get; set; }
            public bool TriggerThresholdWarning { get; set; }

        }
    }
}
