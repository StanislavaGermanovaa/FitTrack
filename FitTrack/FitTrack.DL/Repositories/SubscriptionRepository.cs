using FitTrack.Models.DTO;
using MongoDB.Driver;
namespace FitTrack.DL.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IMongoCollection<Subscription> _subscriptions;

        public SubscriptionRepository(IMongoDatabase database)
        {
            _subscriptions = database.GetCollection<Subscription>("Subscriptions");
        }

        public IEnumerable<Subscription> GetAll()
        {
            return _subscriptions.Find(_ => true).ToList();
        }

        public Subscription GetById(string id)
        {
            return _subscriptions.Find(subscription => subscription.Id == id).FirstOrDefault();
        }

        public void Create(Subscription entity)
        {
            _subscriptions.InsertOne(entity);
        }

        public void Update(string id, Subscription entity)
        {
            _subscriptions.ReplaceOne(subscription => subscription.Id == id, entity);
        }

        public void Delete(string id)
        {
            _subscriptions.DeleteOne(subscription => subscription.Id == id);
        }
    }
}
