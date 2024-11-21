using ClassValuationWeather.Domain.Entities;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IDataRepository
    {
        Task<WeatherItem>? GetWeatherInfoByLngLatTime(float longitude, float latitude, string time);

        Task<List<WeatherItem>>? GetWeatherInfoByCityTime(string city, string time);

        Task<List<CityCoordinates>>? GetCityCoordinates(string city);

        Task SaveWeatherInfoByCoordinates(List<WeatherItem> items);

        Task SaveCityCoordinates(List<CityCoordinates> cityCoordinates);
    }
}
