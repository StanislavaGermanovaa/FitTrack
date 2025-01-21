using FitTrack.BL.Interfaces;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;


namespace FitTrack.BL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public IEnumerable<Subscription> GetAllSubscriptions()
        {
            return _subscriptionRepository.GetAll();
        }

        public Subscription GetSubscriptionById(string id)
        {
            return _subscriptionRepository.GetById(id);
        }

        public void CreateSubscription(Subscription subscription)
        {
            _subscriptionRepository.Create(subscription);
        }


        public void DeleteSubscription(string id)
        {
            _subscriptionRepository.Delete(id);
        }
    }
}
