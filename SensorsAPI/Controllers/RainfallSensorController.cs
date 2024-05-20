using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;

namespace SensorsAPI.Controllers
{
    [Route("api/rainfall-sensor")]
    [ApiController]
    public class RainfallSensorController : ControllerBase // Changed from Controller to ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RainfallSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<float>> GetRainfall(int sensorId)
        {
            var rainfallSensor = new RainfallSensor(); // Create instance of RainfallSensor
            float rainfallValue = rainfallSensor.GetRainfall();

            bool triggerWarning = false;        //constant for humidity as normal threshold is N/A

            if (rainfallValue < 0 || rainfallValue > 32)
            {
                triggerWarning = true;
                Console.WriteLine($"Warning: Rainfall levels have crossed the normal threshold with {rainfallValue}mm. Immediate attention required.");

            }

            TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

            var rainfallData = new RainfallData
            {
                SensorId = sensorId,
                Timestamp = ukTime,
                Rainfall = rainfallValue,
                TriggerThresholdWarning = triggerWarning
            };

            // Serialize temperature data to JSON
            var jsonContent = JsonSerializer.Serialize(rainfallData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to Monitoring Station API
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7051/"); // Replace with Monitoring Station API base URL

                var response = await httpClient.PostAsync("/api/rainfall-monitoring", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(rainfallData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to send rainfall data to Monitoring Station API.");
                }
            }

        }

        public class RainfallData
        {
            public int Id { get; set; }
            public int SensorId { get; set; }
            public DateTime Timestamp { get; set; }
            public float Rainfall { get; set; }
            public bool TriggerThresholdWarning { get; set; }

        }
    }
}
