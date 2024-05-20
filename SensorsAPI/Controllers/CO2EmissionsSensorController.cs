using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;

namespace SensorsAPI.Controllers
{
    [Route("api/co2-emissions-sensor")]
    [ApiController]
    public class CO2EmissionsSensorController : ControllerBase // Changed from Controller to ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CO2EmissionsSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<float>> GetCO2Emissions(int sensorId)
        {
            var cO2EmissionsSensor = new CO2EmissionsSensor(); // Create instance of CO2EmissionsSensor
            float cO2EmissionsValue = cO2EmissionsSensor.GetCO2Emissions();

            bool triggerWarning = false;        //constant for cO2Emissions as normal threshold is N/A

            TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

            var cO2EmissionsData = new CO2EmissionsData
            {
                SensorId = sensorId,
                Timestamp = ukTime,
                CO2Emissions = cO2EmissionsValue,
                TriggerThresholdWarning = triggerWarning
            };

            // Serialize temperature data to JSON
            var jsonContent = JsonSerializer.Serialize(cO2EmissionsData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to Monitoring Station API
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7051/"); // Replace with Monitoring Station API base URL

                var response = await httpClient.PostAsync("/api/co2-emissions-monitoring", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(cO2EmissionsData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to send co2 emissions data to Monitoring Station API.");
                }
            }

        }

        public class CO2EmissionsData
        {
            public int Id { get; set; }
            public int SensorId { get; set; }
            public DateTime Timestamp { get; set; }
            public float CO2Emissions { get; set; }
            public bool TriggerThresholdWarning { get; set; }

        }
    }
}
