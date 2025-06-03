using FitTrack.BL.Interfaces;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();

    public Task<User?> GetUserByIdAsync(string id) => _userRepository.GetByIdAsync(id);

    public Task CreateUserAsync(User user) => _userRepository.CreateAsync(user);

    public Task DeleteUserAsync(string id) => _userRepository.DeleteAsync(id);
}
