using FitTrack.Models.DTO;

namespace FitTrack.DL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User entity);
        Task DeleteAsync(string id);
    }

}
