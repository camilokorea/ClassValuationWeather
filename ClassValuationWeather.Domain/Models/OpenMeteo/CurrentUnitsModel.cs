using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Domain.Models.OpenMeteo
{
    public class CurrentUnitsModel
    {
        public string? Time { get; set; } = "iso8601";
        public string? Interval { get; set; } = "seconds";
        public string? Temperature_2m { get; set; } = "°C";
        public string? Wind_speed_10m { get; set; } = "km/h";
        public string? Wind_direction_10m { get; set; } = "°";
    }
}
