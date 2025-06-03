using FitTrack.BL.Interfaces;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;
using FitTrack.Models.Request;

namespace FitTrack.BL.Services
{
    public class BlUserService : IBlUserService
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public BlUserService(IUserService userService, ISubscriptionService subscriptionService, ISubscriptionRepository subscriptionRepository)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<List<Subscription>> GetUserWithSubscriptionsAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return new();

            var allSubs = await _subscriptionService.GetAllSubscriptionsAsync();
            return allSubs.Where(sub => sub.UserId == userId).ToList();
        }

        public async Task<bool> UpdateSubscriptionForUserAsync(string userId, SubscriptionRequest request)
        {
            var allSubs = await _subscriptionRepository.GetAllAsync();
            var subscription = allSubs.FirstOrDefault(sub => sub.UserId == userId);

            if (subscription == null) return false;

            subscription.Type = request.Type;
            subscription.StartDate = request.StartDate;
            subscription.EndDate = request.EndDate;
            subscription.IsActive = request.IsActive;

            await _subscriptionRepository.UpdateSubscriptionAsync(subscription);
            return true;
        }
    }

}

