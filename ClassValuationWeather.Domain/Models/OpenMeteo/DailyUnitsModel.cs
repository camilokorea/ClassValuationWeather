using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Domain.Models.OpenMeteo
{
    public class DailyUnitsModel
    {
        public string? Time { get; set; } = "iso8601";
        public string? Sunrise { get; set; } = "iso8601";
    }
}
