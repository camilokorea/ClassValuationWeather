using ClassValuationWeather.Domain.Entities;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IOpenMeteoRepository
    {
        public Task<List<WeatherItem>> GetWeatherInfoByCoordinates(List<Coordinates> coordinates);
        public Task<List<CityCoordinates>> GetCitiesByCityName(string cityName);
    }
}
