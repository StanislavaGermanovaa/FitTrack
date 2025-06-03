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

        public Task<List<Subscription>> GetAllSubscriptionsAsync() => _subscriptionRepository.GetAllAsync();

        public Task<Subscription?> GetSubscriptionByIdAsync(string id) => _subscriptionRepository.GetByIdAsync(id);

        public Task CreateSubscriptionAsync(Subscription subscription) => _subscriptionRepository.CreateAsync(subscription);

        public Task DeleteSubscriptionAsync(string id) => _subscriptionRepository.DeleteAsync(id);
    }

}
