using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

                    if (cityCoordinates == null)
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

                    weatherItems.ForEach(weatherItem =>
                    {
                        cityCoordinates.ForEach((coordinatesItem) =>
                        {
                            if (weatherItem.Latitude == coordinatesItem.Latitude && weatherItem.Longitude == coordinatesItem.Longitude)
                            {
                                response.Add(new WeatherItemResponseByCity
                                {
                                    Admin1 = coordinatesItem.Admin1,
                                    Admin2 = coordinatesItem.Admin2,
                                    Admin3 = coordinatesItem.Admin3,
                                    City = coordinatesItem.City,
                                    Country = coordinatesItem.Country,
                                    SunriseDateTime = weatherItem.SunriseDateTime,
                                    Temperature = weatherItem.Temperature,
                                    WindDirection = weatherItem.WindDirection,
                                    WindSpeed = weatherItem.WindSpeed
                                });

                                weatherItems[weatherItems.IndexOf(weatherItems.Find(x => x.Latitude == weatherItem.Latitude && x.Longitude == weatherItem.Longitude))] = weatherItem;
                            }
                        });
                    });

                    await _dataRepository.SaveWeatherInfoByCoordinates(weatherItems);
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

    }
}
