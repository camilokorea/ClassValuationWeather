using ClassValuationWeather.Application.DTO;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IMeteoService
    {
        Task<WeatherItemResponse?> SynchDataByLoc(float longitude, float latitude);

        Task<List<WeatherItemResponseByCity>?> SynchDataByCity(string cityName);
    }
}
