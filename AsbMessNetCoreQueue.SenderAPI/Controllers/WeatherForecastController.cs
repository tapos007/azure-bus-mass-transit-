using AsbMessNetCoreQueue.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AsbMessNetCoreQueue.SenderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IBus _bus;
        // private readonly ISendEndpoint _sendEndpoint;


        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IPublishEndpoint publishEndpoint, IBus bus)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _bus = bus;
            //  _sendEndpoint = sendEndpoint;
        }

        [HttpGet("GetWeatherSendForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherSendForecast()
        {
            Random rand = new Random();
            // await _publishEndpoint.Publish<FlightOrder>(
            //     new FlightOrder { FlightId = Guid.NewGuid(), OrderId = rand.Next(1, 999) });
            var sendEndpoint = await _bus.GetSendEndpoint(new Uri("sb://demobus007.servicebus.windows.net/asbmessnetcorequeue.contracts.flightcancellation"));
            await sendEndpoint.Send<FlightCancellation>(new FlightCancellation
                { FlightId = Guid.NewGuid(), CancellationId = rand.Next(1, 999) });
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }


        [HttpGet("GetWeatherPublishForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherPublishForecast()
        {
            Random rand = new Random();
            await _publishEndpoint.Publish<FlightOrder>(
                new FlightOrder { FlightId = Guid.NewGuid(), OrderId = rand.Next(1, 999) });
          
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}