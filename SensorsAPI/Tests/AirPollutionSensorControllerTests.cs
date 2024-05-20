using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace SensorsAPI.Tests
{
    public class AirPollutionSensorControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AirPollutionSensorControllerTests()
        {
        }

        [Fact]
        public async Task GetTemperature_ReturnsCorrectResult()
        { 
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK);
            fakeResponse.Content = new StringContent("");
            httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(new HttpClient(new FakeHttpMessageHandler(fakeResponse)));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(httpClientFactoryMock.Object);
                });
            }).CreateClient();

            var sensorId = 7;
             
            var response = await client.GetAsync($"/api/humidity-sensor?_sensorId={sensorId}");
             
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }

}
