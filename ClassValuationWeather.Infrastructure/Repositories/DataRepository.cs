using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Domain.Entities;
using MongoDB.Driver;

namespace ClassValuationWeather.Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;
        private readonly IMongoDatabase? _database;
        private readonly IMongoCollection<WeatherItem>? _weatherItemCollection;
        private readonly IMongoCollection<CityCoordinates>? _cityCoordinatesCollection;

        public DataRepository(string connectionString)
        {
            try
            {
                _connectionString = connectionString;

                var mongoUrl = MongoUrl.Create(connectionString);
                var mongoClient = new MongoClient(mongoUrl);

                _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

                _weatherItemCollection = _database.GetCollection<WeatherItem>("WeatherItems");

                _weatherItemCollection = _database.GetCollection<WeatherItem>("CityCoordinates");

                CreateIndexes();
            }
            catch
            {
                throw;
            }
        }

        public IMongoDatabase? Database => _database;

        private void CreateIndexes()
        {
            var geoLocIndexKeys = Builders<WeatherItem>.IndexKeys
                .Descending(e => e.Time)
                .Ascending(e => e.Latitude)
                .Ascending(e => e.Longitude);

            var geoLocIndexModel = new CreateIndexModel<WeatherItem>(geoLocIndexKeys);
            _weatherItemCollection?.Indexes.CreateOne(geoLocIndexModel);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            var cityLocIndexKeys = Builders<WeatherItem>.IndexKeys
                .Descending(e => e.Time)
                .Ascending(e => e.City);

            var cityLocIndexModel = new CreateIndexModel<WeatherItem>(cityLocIndexKeys);
            _weatherItemCollection?.Indexes.CreateOne(cityLocIndexModel);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            var cityIndexKeys = Builders<CityCoordinates>.IndexKeys
                .Ascending(e => e.City)
                .Ascending(e => e.Country)
                .Ascending(e => e.Admin1)
                .Ascending(e => e.Admin2)
                .Ascending(e => e.Admin3);

            var cityIndexModel = new CreateIndexModel<CityCoordinates>(cityIndexKeys);
            _cityCoordinatesCollection?.Indexes.CreateOne(cityIndexModel);
        }

        public async Task<WeatherItem>? GetWeatherInfoByLngLatTime(float longitude, float latitude, string time)
        {
            try
            {
                var filter = Builders<WeatherItem>.Filter.Eq(x => x.Latitude, latitude);
                filter &= (Builders<WeatherItem>.Filter.Eq(x => x.Longitude, longitude));
                filter &= (Builders<WeatherItem>.Filter.Eq(x => x.Time, time));

                var result = await _weatherItemCollection.FindAsync(filter);

                return result.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<WeatherItem>>? GetWeatherInfoByCityTime(string city, string time)
        {
            try
            {
                var filter = Builders<WeatherItem>.Filter.Eq(x => x.City, city);
                filter &= (Builders<WeatherItem>.Filter.Eq(x => x.Time, time));

                var responseItems = await _weatherItemCollection.FindAsync(filter);

                return responseItems.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CityCoordinates>>? GetCityCoordinates(string city)
        {
            try
            {
                var filter = Builders<CityCoordinates>.Filter.Eq(x => x.City, city);

                var result = await _cityCoordinatesCollection.FindAsync(filter);

                return result.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task SaveWeatherInfoByCoordinates(List<WeatherItem> items)
        {
            try
            {
                await _weatherItemCollection.InsertManyAsync(items);
            }
            catch
            {
                throw;
            }
        }

        public async Task SaveCityCoordinates(List<CityCoordinates> cityCoordinates)
        {
            try
            {
                await _cityCoordinatesCollection.InsertManyAsync(cityCoordinates);
            }
            catch
            {
                throw;
            }
        }
    }
}
