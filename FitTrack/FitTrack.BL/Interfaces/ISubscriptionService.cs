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
        IEnumerable<Subscription> GetAllSubscriptions();
        Subscription GetSubscriptionById(string id);
        void CreateSubscription(Subscription subscription);
        void DeleteSubscription(string id);
    }
}
