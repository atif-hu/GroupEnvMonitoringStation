using SensorsAPI.Controllers;
using SensorsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<TemperatureSensorController>(); // Register TemperatureSensorController
builder.Services.AddHostedService<TemperatureReadingService>();

builder.Services.AddSingleton<HumiditySensorController>(); // Register HumiditySensorController
builder.Services.AddHostedService<HumidityReadingService>();

builder.Services.AddSingleton<RainfallSensorController>(); // Register RainfallSensorController
builder.Services.AddHostedService<RainfallReadingService>();

builder.Services.AddSingleton<AirPollutionSensorController>(); // Register AirPollutionSensorController
builder.Services.AddHostedService<AirPollutionReadingService>();

builder.Services.AddSingleton<CO2EmissionsSensorController>(); // Register CO2EmissionsSensorController
builder.Services.AddHostedService<CO2EmissionsReadingService>();

var app = builder.Build();

// Configure CORS
app.UseCors(builder => builder
    .WithOrigins("http://localhost:3030") // Replace with your React app URL
    .AllowAnyHeader()
    .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
