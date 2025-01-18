using FitTrack.Models.DTO;

namespace FitTrack.DL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
        void Create(User entity);
        void Update(string id, User entity);
        void Delete(string id);
    }
}
