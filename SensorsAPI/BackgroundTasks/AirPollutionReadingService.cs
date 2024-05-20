using SensorsAPI.Controllers;

namespace SensorsAPI.Services
{
    public class AirPollutionReadingService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public AirPollutionReadingService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var airPollutionSensorController = scope.ServiceProvider.GetRequiredService<AirPollutionSensorController>();
                    await airPollutionSensorController.GetAirPollution(7);

                    // Wait for 60 minute before next iteration
                    await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                }
            }
        }
    }
}
