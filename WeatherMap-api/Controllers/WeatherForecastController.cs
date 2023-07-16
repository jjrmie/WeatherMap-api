using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WeatherMap_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherRepository _weatherRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public WeatherForecastController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet(Name = "GetWeatherByCountryCity")]
        public Task<WeatherMapResponseModel> Get(string country, string city)
        {
            return _weatherRepository.GetWeatherByCountryCity(country, city);
        }
    }
}