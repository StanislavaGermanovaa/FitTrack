using FitTrack.Models.DTO;
using FitTrack.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.BL.Interfaces
{
    public interface IBlUserService
    {
        Task<List<Subscription>> GetUserWithSubscriptionsAsync(string userId);
        Task<bool> UpdateSubscriptionForUserAsync(string userId, SubscriptionRequest request);
    }

}
