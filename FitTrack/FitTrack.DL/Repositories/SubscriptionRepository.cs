using FitTrack.Models.Configurations;
using FitTrack.Models.DTO;
using MongoDB.Driver;
using FitTrack.DL.Interfaces;
using Microsoft.Extensions.Options;

namespace FitTrack.DL.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IMongoCollection<Subscription> _subscriptions;

        public SubscriptionRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _subscriptions = database.GetCollection<Subscription>($"{nameof(Subscription)}s");
        }

        public List<Subscription> GetAll()
        {
            return _subscriptions.Find(_ => true).ToList();
        }

        public Subscription GetById(string id)
        {
            return _subscriptions.Find(subscription => subscription.Id == id).FirstOrDefault();
        }

        public void Create(Subscription subscription)
        {
            subscription.Id=Guid.NewGuid().ToString();
            _subscriptions.InsertOne(subscription);
        }


        public void Delete(string id)
        {
            _subscriptions.DeleteOne(subscription => subscription.Id == id);
        }
    }
}
