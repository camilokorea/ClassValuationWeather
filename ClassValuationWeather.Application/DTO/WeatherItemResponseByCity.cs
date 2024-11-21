using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Application.DTO
{
    public class WeatherItemResponseByCity : WeatherItemResponse
    {
        public WeatherItemResponseByCity() { }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Admin1 { get; set; }

        public string? Admin2 { get; set; }

        public string? Admin3 { get; set; }
    }
}
