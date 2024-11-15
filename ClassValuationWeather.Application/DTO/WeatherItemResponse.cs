using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.DTO
{
    public class WeatherItemResponse
    {
        public float? Temperature { get; set; }
        public int? WindDirection { get; set; }
        public float? WindSpeed { get; set; }
        public string? SunriseDateTime { get; set; }
    }
}
