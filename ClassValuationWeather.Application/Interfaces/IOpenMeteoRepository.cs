using ClassValuationWeather.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IOpenMeteoRepository
    {
        public Task<WeatherItem> GetWeatherInfoByCoordinates(float longitude, float latitude);
    }
}
