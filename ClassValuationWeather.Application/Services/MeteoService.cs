using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.Services
{
    public class MeteoService : IMeteoService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IOpenMeteoRepository _openMeteoRepository;
        private readonly string _time = DateTime.Now.ToString("yyyy-MM-dd");
        public MeteoService(IDataRepository dataRepository, IOpenMeteoRepository openMeteoRepository)
        {
            _dataRepository = dataRepository;
            _openMeteoRepository = openMeteoRepository;
        }

        public async Task<WeatherItemResponse?> SynchDataByLoc(float longitude, float latitude)
        {
            try
            {
                WeatherItem? weatherItem = await _dataRepository.GetWeatherInfoByLngLatTime(longitude, latitude, _time);

                if (weatherItem != null)
                {
                    return new WeatherItemResponse
                    {
                        SunriseDateTime = weatherItem?.SunriseDateTime,
                        Temperature = weatherItem?.Temperature,
                        WindDirection = weatherItem?.WindDirection,
                        WindSpeed = weatherItem?.WindSpeed
                    };
                }
                else
                {
                    List<Coordinates> coordinates = new List<Coordinates>();

                    coordinates.Add(new Coordinates
                    {
                        Latitude = latitude,
                        Longitude = longitude
                    });

                    var weatherItems = await _openMeteoRepository.GetWeatherInfoByCoordinates(coordinates);

                    if (weatherItems.Count > 0)
                    {
                        weatherItem = weatherItems.Single();

                        await _dataRepository.SaveWeatherInfoByCoordinates(weatherItems);

                        return new WeatherItemResponse
                        {
                            SunriseDateTime = weatherItem?.SunriseDateTime,
                            Temperature = weatherItem?.Temperature,
                            WindDirection = weatherItem?.WindDirection,
                            WindSpeed = weatherItem?.WindSpeed
                        };
                    }
                    else
                    {
                        return new WeatherItemResponse
                        {
                            SunriseDateTime = null,
                            Temperature = null,
                            WindDirection = null,
                            WindSpeed = null
                        };
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<WeatherItemResponseByCity>?> SynchDataByCity(string cityName)
        {
            try
            {
                List<WeatherItemResponseByCity> response = new List<WeatherItemResponseByCity>();

                List<WeatherItem>? weatherItems = await _dataRepository.GetWeatherInfoByCityTime(cityName, _time);

                if (weatherItems.Count > 0)
                {
                    weatherItems?.ForEach(weatherItem =>
                    {
                        response.Add(new WeatherItemResponseByCity
                        {
                            Admin1 = weatherItem.Admin1,
                            Admin2 = weatherItem.Admin2,
                            Admin3 = weatherItem.Admin3,
                            City = weatherItem.City,
                            Country = weatherItem.Country,
                            SunriseDateTime = weatherItem.SunriseDateTime,
                            Temperature = weatherItem.Temperature,
                            WindDirection = weatherItem.WindDirection,
                            WindSpeed = weatherItem.WindSpeed
                        });
                    });
                }
                else
                {
                    List<CityCoordinates>? cityCoordinates = await _dataRepository.GetCityCoordinates(cityName);

                    if (cityCoordinates.Count == 0)
                    {
                        cityCoordinates = await _openMeteoRepository.GetCitiesByCityName(cityName);

                        await _dataRepository.SaveCityCoordinates(cityCoordinates);
                    }

                    List<Coordinates> coordinates = new List<Coordinates>();

                    cityCoordinates.ForEach(coordinatesItem =>
                    {
                        coordinates.Add(new Coordinates
                        {
                            Latitude = coordinatesItem.Latitude,
                            Longitude = coordinatesItem.Longitude
                        });
                    });

                    weatherItems = await _openMeteoRepository.GetWeatherInfoByCoordinates(coordinates);


                    for (int i = 0; i < weatherItems.Count; i++)
                    {
                        response.Add(new WeatherItemResponseByCity
                        {
                            Admin1 = cityCoordinates[i].Admin1,
                            Admin2 = cityCoordinates[i].Admin2,
                            Admin3 = cityCoordinates[i].Admin3,
                            City = cityCoordinates[i].City,
                            Country = cityCoordinates[i].Country,
                            SunriseDateTime = weatherItems[i].SunriseDateTime,
                            Temperature = weatherItems[i].Temperature,
                            WindDirection = weatherItems[i].WindDirection,
                            WindSpeed = weatherItems[i].WindSpeed
                        });

                        weatherItems[i].Admin1 = cityCoordinates[i].Admin1;
                        weatherItems[i].Admin2 = cityCoordinates[i].Admin2;
                        weatherItems[i].Admin3 = cityCoordinates[i].Admin3;
                        weatherItems[i].City = cityCoordinates[i].City;
                        weatherItems[i].Country = cityCoordinates[i].Country;
                    }

                    await _dataRepository.SaveWeatherInfoByCoordinates(weatherItems);
                }

                return response.FindAll(x => x.City == cityName);
            }
            catch
            {
                throw;
            }
        }

    }
}
