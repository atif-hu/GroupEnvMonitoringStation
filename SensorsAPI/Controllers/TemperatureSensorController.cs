using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;

namespace SensorsAPI.Controllers
{
    [Route("api/temperature-sensor")]
    [ApiController]
    public class TemperatureSensorController : ControllerBase // Changed from Controller to ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TemperatureSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<float>> GetTemperature(int _sensorId)
        {
            try
            {
                var temperatureSensor = new TemperatureSensor(); // Create instance of TemperatureSensor
                float temperatureValue = temperatureSensor.GetTemperature();

                bool triggerWarning = false;

                if(temperatureValue < -9 || temperatureValue > 30)
                {
                    triggerWarning = true;
                    Console.WriteLine($"Warning: Temperature levels have crossed the normal threshold with {temperatureValue} de. Immediate attention required.");
                }

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var temperatureData = new TemperatureData
                {
                    SensorId = _sensorId,
                    Timestamp = ukTime,
                    Temperature = temperatureValue,
                    TriggerThresholdWarning = triggerWarning
                };

                // Serialize temperature data to JSON
                var jsonContent = JsonSerializer.Serialize(temperatureData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request to Monitoring Station API
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7051/"); // Replace with Monitoring Station API base URL

                    var response = await httpClient.PostAsync("/api/temperature-monitoring", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Temperature data with sensor id {_sensorId} added successfully -> ",response.Content);
                        return Ok(temperatureData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to send temperature data to Monitoring Station API.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }

        public class TemperatureData
        {
            public int Id { get; set; }
            public int SensorId { get; set; }
            public DateTime Timestamp { get; set; }
            public float Temperature { get; set; }
            public bool TriggerThresholdWarning { get; set; }

        }
    }
}
