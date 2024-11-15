using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.Services
{
    public class MeteoService : IMeteoService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IOpenMeteoRepository _openMeteoRepository;
        public MeteoService(IDataRepository dataRepository, IOpenMeteoRepository openMeteoRepository)
        {
            _dataRepository = dataRepository;
            _openMeteoRepository = openMeteoRepository;
        }

        public async Task<WeatherItemResponse?> SynchData(float longitude, float latitude)
        {
            try
            {
                var time = DateTime.Now.ToString("yyyy-MM-dd");

                var weatherItem = await _dataRepository.GetByLngLatTime(longitude, latitude, time);

                if (weatherItem != null)
                {
                    return new WeatherItemResponse
                    {
                        SunriseDateTime = weatherItem.SunriseDateTime,
                        Temperature = weatherItem.Temperature,
                        WindDirection = weatherItem.WindDirection,
                        WindSpeed = weatherItem.WindSpeed
                    };
                }
                else
                {
                    weatherItem = await _openMeteoRepository.GetWeatherInfoByCoordinates(longitude, latitude);

                    if (weatherItem != null)
                    {
                        await _dataRepository.SaveWeatherInfoByCoordinates(weatherItem);
                    }

                    return new WeatherItemResponse
                    {
                        SunriseDateTime = weatherItem.SunriseDateTime,
                        Temperature = weatherItem.Temperature,
                        WindDirection = weatherItem.WindDirection,
                        WindSpeed = weatherItem.WindSpeed
                    };
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
