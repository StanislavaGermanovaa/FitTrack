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

        public BlUserService(IUserService userService, ISubscriptionService subscriptionService,ISubscriptionRepository subscriptionRepository)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _subscriptionRepository = subscriptionRepository;
        }

        public List<Subscription> GetUserWithSubscriptions(string userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return new List<Subscription>();
            }

            return _subscriptionService.GetAllSubscriptions()
                                       .Where(subscription => subscription.UserId == userId)
                                       .ToList();
        }

        public bool UpdateSubscriptionForUser(string userId, SubscriptionRequest request)
        {
            var subscription = _subscriptionRepository.GetAll()
                                                      .FirstOrDefault(sub => sub.UserId == userId);
            if (subscription == null)
            {
                return false;
            }

            subscription.Type = request.Type;
            subscription.StartDate = request.StartDate;
            subscription.EndDate = request.EndDate;
            subscription.IsActive = request.IsActive;

            _subscriptionRepository.UpdateSubscription(subscription);
            return true;
        }
    }
}

