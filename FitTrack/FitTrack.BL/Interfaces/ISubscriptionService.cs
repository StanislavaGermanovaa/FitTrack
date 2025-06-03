using FitTrack.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.BL.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription?> GetSubscriptionByIdAsync(string id);
        Task CreateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(string id);
    }

}
