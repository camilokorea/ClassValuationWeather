using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Domain.Models.OpenMeteo
{
    public class OpenMeteoResponseModel
    {
        public float? Latitude { get; set; } = 0;
        public float? Longitude { get; set; } = 0;
        public float? Generationtime_ms { get; set; } = 0;
        public int? Utc_offset_seconds { get; set; } = 0;
        public string? Timezone { get; set; }
        public string? Timezone_abbreviation { get; set; }
        public int? Elevation { get; set; }
        public CurrentUnitsModel Current_units { get; set; }
        public CurrentModel Current { get; set; }
        public DailyUnitsModel Daily_units { get; set; }
        public DailyModel Daily { get; set; }
    }
}
