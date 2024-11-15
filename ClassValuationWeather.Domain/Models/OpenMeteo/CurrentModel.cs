using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Domain.Models.OpenMeteo
{
    public class CurrentModel
    {
        public string? Time { get; set; } = string.Empty;
        public int? Interval { get; set; }
        public int? Temperature_2m { get; set; }
        public float? Wind_speed_10m { get; set; }
        public int? Wind_direction_10m { get; set; }
    }
}
