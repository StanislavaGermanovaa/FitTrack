using FitTrack.Models.DTO;

namespace FitTrack.DL.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(string id);
        void Create(User entity);
        void Delete(string id);
    }
}
