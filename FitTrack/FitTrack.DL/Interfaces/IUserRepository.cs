using FitTrack.Models.DTO;
using FitTrack.DL.Cache;

namespace FitTrack.DL.Interfaces
{
    public interface IUserRepository : ICacheRepository<string, User>
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User entity);
        Task DeleteAsync(string id);
    }

}
