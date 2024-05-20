using SensorsAPI.Controllers;
namespace SensorsAPI.Services
{
    public class TemperatureReadingService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public TemperatureReadingService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var temperatureSensorController = scope.ServiceProvider.GetRequiredService<TemperatureSensorController>();
                    await temperatureSensorController.GetTemperature(1);
                    await temperatureSensorController.GetTemperature(2);
                    await temperatureSensorController.GetTemperature(3);

                    // Wait for 30 minute before next iteration
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }
    }
}
