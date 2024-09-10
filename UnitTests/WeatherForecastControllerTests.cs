using Moq;
using Xunit;
using APIDesign.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using APIDesign;
public class WeatherForecastControllerTests
{
    private readonly WeatherForecastController _controller;
    private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
    private readonly HttpClient _client;

    public WeatherForecastControllerTests()
    {
        _mockLogger = new Mock<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(_mockLogger.Object);
        _client = new HttpClient();
    }

    [Fact]
    public void GetReturnsWeatherForecasts()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());

        foreach (var forecast in result)
        {
            Assert.Contains(forecast.Summary, new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" });
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsExpectedNumberOfForecasts()
    {

        var response = _controller.Get();
        IEnumerable<WeatherForecast> forecasts = response;

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Count());
    }

    [Fact]
    public async Task GetPosts_ReturnsSuccessStatusCodeAndCorrectContentType()
    {
        // Arrange
        var requestUri = "https://jsonplaceholder.typicode.com/posts";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
                     response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetPosts_ReturnsNonEmptyResponse()
    {
        // Arrange
        var requestUri = "https://jsonplaceholder.typicode.com/posts";

        // Act
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.False(string.IsNullOrEmpty(content));
        Assert.Contains("body", content);
    }
}

