using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Domain.Entities;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ClassValuationWeather.Infrastructure.Repositories
{
    public class OpenMeteoRepository : IOpenMeteoRepository
    {
        public async Task<List<WeatherItem>> GetWeatherInfoByCoordinates(List<Coordinates> coordinates)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var weatherItems = new List<WeatherItem>();

                    string openMeteoURL = "https://api.open-meteo.com/v1/forecast?";
                    string coordinatesString = "";

                    coordinates.ForEach(coordinate =>
                    {
                        coordinatesString += "latitude=" + coordinate.Latitude?.ToString(CultureInfo.InvariantCulture) + "&longitude=" + coordinate.Longitude?.ToString(CultureInfo.InvariantCulture) + "&";
                    });

                    openMeteoURL += coordinatesString + "current=temperature_2m,wind_speed_10m,wind_direction_10m&daily=sunrise&timezone=GMT&forecast_days=1";

                    HttpResponseMessage response = await client.GetAsync(openMeteoURL);

                    response.EnsureSuccessStatusCode();

                    string jsonString = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(jsonString);

                    weatherItems.Add(new WeatherItem
                    {
                        Latitude = (float?)json["latitude"],
                        Longitude = (float?)json["longitude"],
                        Time = (string?)json["daily"]?["time"]?[0],
                        SunriseDateTime = (string?)json["daily"]?["sunrise"]?[0],
                        Temperature = (float?)json["current"]?["temperature_2m"],
                        WindDirection = (int?)json["current"]?["wind_direction_10m"],
                        WindSpeed = (float?)json["current"]?["wind_speed_10m"]
                    });

                    return weatherItems;
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<List<CityCoordinates>> GetCitiesByCityName(string cityName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    List<CityCoordinates> cityCoordinates = new List<CityCoordinates>();

                    string openMeteoURL = "https://geocoding-api.open-meteo.com/v1/search?name=" + cityName + "&count=100&language=en&format=json";

                    HttpResponseMessage response = await client.GetAsync(openMeteoURL);

                    response.EnsureSuccessStatusCode();

                    string jsonString = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(jsonString);

                    if (json != null)
                    {
                        cityCoordinates.AddRange(from resultItem in json["results"]
                                              select new CityCoordinates
                                              {
                                                  Latitude = (float?)resultItem["latitude"],
                                                  Longitude = (float?)resultItem["longitude"],
                                                  City = (string?)resultItem["city"],
                                                  Country = (string?)resultItem["country"],
                                                  Admin1 = (string?)resultItem["admin1"],
                                                  Admin2 = (string?)resultItem["admin2"],
                                                  Admin3 = (string?)resultItem["admin3"]
                                              });
                    }

                    return cityCoordinates;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
