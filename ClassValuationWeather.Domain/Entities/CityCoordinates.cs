using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClassValuationWeather.Domain.Entities
{
    public class CityCoordinates
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Id { get; set; }

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

        [BsonElement("latitude"), BsonRepresentation(BsonType.Double)]
        public float? Latitude { get; set; }

        [BsonElement("longitude"), BsonRepresentation(BsonType.Double)]
        public float? Longitude { get; set; }
    }
}
