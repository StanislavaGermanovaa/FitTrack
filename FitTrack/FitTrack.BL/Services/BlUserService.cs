using FitTrack.BL.Interfaces;
using FitTrack.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.BL.Services
{
    public class BlUserService : IBlUserService
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;

        public BlUserService(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
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
    }
}

