using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClassValuationWeather.Domain.Entities
{
    public class WeatherItem
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Id { get; set; }

        [BsonElement("time"), BsonRepresentation(BsonType.String)]
        public string? Time { get; set; }

        [BsonElement("latitude"), BsonRepresentation(BsonType.Double)]
        public float? Latitude { get; set; }

        [BsonElement("longitude"), BsonRepresentation(BsonType.Double)]
        public float? Longitude { get; set; }

        [BsonElement("temperature"), BsonRepresentation(BsonType.Double)]
        public float? Temperature { get; set; }

        [BsonElement("wind_direction"), BsonRepresentation(BsonType.Int32)]
        public int? WindDirection { get; set; }

        [BsonElement("wind_speed"), BsonRepresentation(BsonType.Double)]
        public float? WindSpeed { get; set; }

        [BsonElement("sunrise_date_time"), BsonRepresentation(BsonType.String)]
        public string? SunriseDateTime { get; set; }

        [BsonElement("city"), BsonRepresentation(BsonType.String)]
        public string? City { get; set; }

        [BsonElement("country"), BsonRepresentation(BsonType.String)]
        public string? Country { get; set; }

        [BsonElement("admin1"), BsonRepresentation(BsonType.String)]
        public string? Admin1 { get; set; }

        [BsonElement("admin2"), BsonRepresentation(BsonType.String)]
        public string? Admin2 { get; set; }

        [BsonElement("admin3"), BsonRepresentation(BsonType.String)]
        public string? Admin3 { get; set; }
    }
}
