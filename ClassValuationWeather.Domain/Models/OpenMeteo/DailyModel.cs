using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Domain.Models.OpenMeteo
{
    public class DailyModel
    {
        public List<string> Time { get; set; } = new List<string>();
        public List<string> Sunrise { get; set; } = new List<string>();
    }
}
