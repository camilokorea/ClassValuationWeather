using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Domain.Models.OpenMeteo;
using ClassValuationWeather.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ClassValuationWeather.Infrastructure.Repositories
{
    public class OpenMeteoRepository : IOpenMeteoRepository
    {
        public async Task<WeatherItem> GetWeatherInfoByCoordinates(float longitude, float latitude)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string openMeteoURL = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude.ToString(CultureInfo.InvariantCulture) + "&longitude=" + longitude.ToString(CultureInfo.InvariantCulture) + "&current=temperature_2m,wind_speed_10m,wind_direction_10m&daily=sunrise&timezone=GMT&forecast_days=1";

                    HttpResponseMessage response = await client.GetAsync(openMeteoURL);

                    response.EnsureSuccessStatusCode();

                    string jsonString = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(jsonString);

                    WeatherItem item = new WeatherItem
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Time = (string?)json["daily"]["time"][0],
                        SunriseDateTime = (string?)json["daily"]["sunrise"][0],
                        Temperature = (float?)json["current"]["temperature_2m"],
                        WindDirection = (int?)json["current"]["wind_direction_10m"],
                        WindSpeed = (float?)json["current"]["wind_speed_10m"]
                    };

                    return item;
                }
                catch (HttpRequestException e)
                {
                    throw;
                }
            }
        }
    }
}
