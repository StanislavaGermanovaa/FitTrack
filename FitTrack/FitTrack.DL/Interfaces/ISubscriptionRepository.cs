using FitTrack.Models.DTO;

namespace FitTrack.DL.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(string id);
        Task CreateAsync(Subscription entity);
        Task DeleteAsync(string id);
        Task UpdateSubscriptionAsync(Subscription subscription);
    }

}
