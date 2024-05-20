using SensorsAPI.Controllers;

namespace SensorsAPI.Services
{
    public class RainfallReadingService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public RainfallReadingService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var rainfallSensorController = scope.ServiceProvider.GetRequiredService<RainfallSensorController>();
                    await rainfallSensorController.GetRainfall(5);
                    await rainfallSensorController.GetRainfall(6);

                    // Wait for 30 minute before next iteration
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }
    }
}
