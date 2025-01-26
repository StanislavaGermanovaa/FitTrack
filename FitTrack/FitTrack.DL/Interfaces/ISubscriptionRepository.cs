using FitTrack.Models.DTO;

namespace FitTrack.DL.Interfaces
{
    public interface ISubscriptionRepository
    {
        List<Subscription> GetAll();
        Subscription GetById(string id);
        void Create(Subscription entity);
        void Delete(string id);

        void UpdateSubscription(Subscription subscription);
    }
}
