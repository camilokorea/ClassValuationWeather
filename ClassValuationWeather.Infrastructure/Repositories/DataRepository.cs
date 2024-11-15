using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassValuationWeather.Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;
        private readonly IMongoDatabase? _database;
        private readonly IMongoCollection<WeatherItem> _collection;

        public DataRepository(string connectionString)
        {
            try
            {
                _connectionString = connectionString;

                var mongoUrl = MongoUrl.Create(connectionString);
                var mongoClient = new MongoClient(mongoUrl);

                _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

                _collection = _database.GetCollection<WeatherItem>("WeatherItems");

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
            var indexKeys = Builders<WeatherItem>.IndexKeys
                .Descending(e => e.Time)
                .Ascending(e => e.Latitude)
                .Ascending(e => e.Longitude);

            var indexModel = new CreateIndexModel<WeatherItem>(indexKeys);
            _collection.Indexes.CreateOne(indexModel);
        }

        public Task<WeatherItem> GetByLngLatTime(float longitude, float latitude, string time)
        {
            try
            {
                var filter = Builders<WeatherItem>.Filter.Eq(x => x.Latitude, latitude);
                filter &= (Builders<WeatherItem>.Filter.Eq(x => x.Longitude, longitude));
                filter &= (Builders<WeatherItem>.Filter.Eq(x => x.Time, time));

                var result = _collection.Find(filter).FirstOrDefaultAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task SaveWeatherInfoByCoordinates(WeatherItem? item)
        {
            try
            {
                if (item != null)
                {
                    await _collection.InsertOneAsync(item);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
