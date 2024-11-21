using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassValuationWeather.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMeteoService _meteoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMeteoService meteoService)
        {
            _logger = logger;
            _meteoService = meteoService;
        }

        [HttpGet(Name = "GetWeatherForecastByLoc")]
        public async Task<WeatherItemResponse?> GetWeatherForecastByLoc(float latitude, float longitude)
        {
            return await _meteoService.SynchDataByLoc(longitude, latitude);
        }

        [HttpGet(Name = "GetWeatherForecastByCity")]
        public async Task<List<WeatherItemResponseByCity>?> GetWeatherForecastByCity(string city)
        {
            return await _meteoService.SynchDataByCity(city);
        }
    }
}
