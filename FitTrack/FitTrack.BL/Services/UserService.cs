using FitTrack.BL.Interfaces;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;

namespace FitTrack.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }

        public void CreateUser(User user)
        {
            _userRepository.Create(user);
        }


        public void DeleteUser(string id)
        {
            _userRepository.Delete(id);
        }
    }
}
