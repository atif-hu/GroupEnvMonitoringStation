using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SensorsAPI.Repository.Interface;
using SensorsAPI.Repository.Service;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MyBackgroundService : BackgroundService
{
    private readonly ILogger<MyBackgroundService> _logger;
    private ITemperature _temperature;
    private IRainfall _rainfall;
    private IHumidity _humidity;
    private ICO2Emission _co2emission;
    private IAirPollution _airPollution;
    private readonly IHttpClientFactory _httpClientFactory;

    public MyBackgroundService(ILogger<MyBackgroundService> logger, ITemperature temperature, IRainfall rainfall, IHumidity Humidity, ICO2Emission CO2Emission, IAirPollution AirPollution, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _temperature = temperature;
        _rainfall = rainfall;
        _humidity = Humidity;
        _co2emission = CO2Emission;
        _airPollution = AirPollution;
        _httpClientFactory = httpClientFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Call TemperatureService every 30 minutes
            var temp = _temperature.GetTemperature();
            _logger.LogInformation("Temperature service called at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            await PostDataToMonitoringService(temp, "Temperature");

            // Call Rainfall services in every 30 minutes
            var Rain = _rainfall.GetRainFall();
            _logger.LogInformation("Rainfall value Collected every at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            await PostDataToMonitoringService(Rain, "Rainfall");

            // Call HUmidity services in every 60 minutes
            var humid = _humidity.GetHumidity();
            _logger.LogInformation("Humidity value Collected every at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            await PostDataToMonitoringService(humid, "Humidity");

            // Call Air pollution services in every 60 minutes
            var Airp = _airPollution.GetAirPollution();
            _logger.LogInformation("Air Pollution value Collected every at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            await PostDataToMonitoringService(Airp, "Air Pollution");

            // Call CO2 Emission services in every 120 minutes
            var co2e = _co2emission.GetCO2Emission();
            _logger.LogInformation("CO2 Emission value Collected every at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            await PostDataToMonitoringService(co2e, "CO2 Emission");
        }
    }

    private async Task PostDataToMonitoringService<T>(T data, string enviromentType)
    {
        try
        {
            var monitoringApiUrl = "http://localhost:5112/api/MonitoringStation/Add";

            // Create HttpClient instance
            var httpClient = _httpClientFactory.CreateClient();

            // Create data object
            var environmentSensor = new { EnvironmentParameter = enviromentType,  Value = data , };

            // Serialize data to JSON
            var jsonContent = JsonConvert.SerializeObject(environmentSensor);

            // Prepare content
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Make POST request
            var response = await httpClient.PostAsync(monitoringApiUrl, content);

            // Check response status
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Data posted to monitoring service successfully.");
            }
            else
            {
                _logger.LogError("Failed to post data to monitoring service. Status code: {statusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while posting data to monitoring service: {errorMessage}", ex.Message);
        }
    }


}
