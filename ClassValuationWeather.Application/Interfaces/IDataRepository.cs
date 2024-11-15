using ClassValuationWeather.Entities;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IDataRepository
    {
        Task<WeatherItem> GetByLngLatTime(float longitude, float latitude, string time);

        public Task SaveWeatherInfoByCoordinates(WeatherItem? item);
    }
}
