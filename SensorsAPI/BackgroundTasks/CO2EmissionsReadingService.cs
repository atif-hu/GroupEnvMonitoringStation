using SensorsAPI.Controllers;

namespace SensorsAPI.Services
{
    public class CO2EmissionsReadingService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public CO2EmissionsReadingService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var cO2EmissionsSensorController = scope.ServiceProvider.GetRequiredService<CO2EmissionsSensorController>();
                    await cO2EmissionsSensorController.GetCO2Emissions(8);
                    await cO2EmissionsSensorController.GetCO2Emissions(9);
                    await cO2EmissionsSensorController.GetCO2Emissions(10);
                    await cO2EmissionsSensorController.GetCO2Emissions(11);
                    await cO2EmissionsSensorController.GetCO2Emissions(12);

                    // Wait for 120 minute before next iteration
                    await Task.Delay(TimeSpan.FromMinutes(120), stoppingToken);
                }
            }
        }
    }
}
