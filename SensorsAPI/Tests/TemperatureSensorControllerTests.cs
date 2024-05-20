using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace SensorsAPI.Tests
{
    public class TemperatureSensorControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TemperatureSensorControllerTests()
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

            var sensorId = 1;

            var response = await client.GetAsync($"/api/temperature-sensor?_sensorId={sensorId}");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }

    // A simple fake HttpMessageHandler for mocking HttpClient
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(_response);
            return responseTask.Task;
        }
    }
}
