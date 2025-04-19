using Domain.Entities;
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
            var indexKeys = Builders<BsonDocument>.IndexKeys.Text("Name");
            var indexModel = new CreateIndexModel<BsonDocument>(indexKeys);
            await collection.Indexes.CreateOneAsync(indexModel);
        }

        public async Task MigrateAsync()
        {
            var collection = _database.GetCollection<BsonDocument>("Investment");
            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var doc in documents)
            {
                var update = Builders<BsonDocument>.Update;
                var updates = new List<UpdateDefinition<BsonDocument>>();
                if (doc.Contains("SenderPaymentDetails"))
                {
                    var purchaseInfo = new PurchaseInfo
                    {
                        Amount = doc["SenderPaymentDetails"]["Amount"].AsDouble,
                        UnitCount = 1,
                        UnitPrice = doc["SenderPaymentDetails"]["Amount"].AsDouble,
                        Remarks = doc["ConfirmationDetails"]["Remarks"].AsString,
                        InvoiceNo = doc["ConfirmationDetails"]["InvoiceNo"].AsString,
                        When = doc["SenderPaymentDetails"]["When"].ToUniversalTime(),
                    };
                    updates.Add(update.Set("PurchaseInfos", new List<PurchaseInfo> { purchaseInfo }));
                    updates.Add(update.Set("UnitCount", 1));
                    updates.Add(update.Set("PreviousInvestmentId", ""));
                    updates.Add(update.Unset("SenderPaymentDetails"));
                    updates.Add(update.Unset("ReceiverPaymentDetails"));
                    updates.Add(update.Unset("SenderPaymentDetails"));
                }

                if (updates.Count > 0)
                {
                    var combinedUpdate = update.Combine(updates);
                    collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", doc["_id"]), combinedUpdate);
                }
            }
            Console.WriteLine("Migration completed.");
        }
    }

}
