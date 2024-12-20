using MongoDB.Bson;
using MongoDB.Driver;

namespace Persistence.Context
{
    public class MongoDbInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoDbInitializer(IMongoDbSettings settings)
        {
            _database =  new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        }

        public async Task InitializeIndexesAsync()
        {
            var collection = _database.GetCollection<BsonDocument>("Category");
            var indexKeys = Builders<BsonDocument>.IndexKeys.Ascending("Name");
            var indexModel = new CreateIndexModel<BsonDocument>(indexKeys);
            await collection.Indexes.CreateOneAsync(indexModel);
        }
    }

}
