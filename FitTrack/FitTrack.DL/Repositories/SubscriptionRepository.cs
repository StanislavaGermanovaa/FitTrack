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

        public async Task<List<Subscription>> GetAllAsync()
        {
            var result = await _subscriptions.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(string id)
        {
            var result = await _subscriptions.FindAsync(s => s.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Subscription subscription)
        {
            subscription.Id = Guid.NewGuid().ToString();
            await _subscriptions.InsertOneAsync(subscription);
        }

        public async Task DeleteAsync(string id)
        {
            await _subscriptions.DeleteOneAsync(s => s.Id == id);
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            var filter = Builders<Subscription>.Filter.Eq(s => s.Id, subscription.Id);
            await _subscriptions.ReplaceOneAsync(filter, subscription);
        }
    }

}
