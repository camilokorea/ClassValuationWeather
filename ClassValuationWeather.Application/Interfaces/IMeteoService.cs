using ClassValuationWeather.Application.DTO;
using ClassValuationWeather.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.Interfaces
{
    public interface IMeteoService
    {
        Task<WeatherItemResponse?> SynchData(float longitude, float latitude);
    }
}
