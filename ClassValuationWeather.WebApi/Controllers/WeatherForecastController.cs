using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ClassValuationWeather.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMeteoService _meteoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMeteoService meteoService)
        {
            _logger = logger;
            _meteoService = meteoService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<WeatherItemResponse?> GetWeatherForecast(float latitude, float longitude)
        {
            return await _meteoService.SynchData(longitude, latitude);
        }
    }
}
