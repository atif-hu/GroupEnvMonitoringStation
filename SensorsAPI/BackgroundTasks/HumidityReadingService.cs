using SensorsAPI.Controllers;

namespace SensorsAPI.Services
{
    public class HumidityReadingService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public HumidityReadingService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var humiditySensorController = scope.ServiceProvider.GetRequiredService<HumiditySensorController>();
                    await humiditySensorController.GetHumdity(4);

                    // Wait for 60 minute before next iteration
                    await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                }
            }
        }
    }
}
