using FitTrack.BL.Services;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;
using Moq;

public class UserServiceUnitTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly List<User> _users;

    public UserServiceUnitTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _users = new List<User>
        {
            new User { Id = "1", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", CreatedAt = DateTime.UtcNow },
            new User { Id = "2", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "0987654321", CreatedAt = DateTime.UtcNow }
        };
    }

    [Fact]
    public void GetAllUsers_ReturnsUserList()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAll()).Returns(_users);
        var userService = new UserService(_userRepositoryMock.Object);

        // Act
        var result = userService.GetAllUsers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_users.Count, result.Count);
    }

    [Fact]
    public void GetUserById_ReturnsUser()
    {
        // Arrange
        var userId = "1";
        _userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(_users.First(u => u.Id == userId));
        var userService = new UserService(_userRepositoryMock.Object);

        // Act
        var result = userService.GetUserById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
    }

    [Fact]
    public void CreateUser_CallsRepositoryCreate()
    {
        // Arrange
        var newUser = new User { Id = "3", FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", PhoneNumber = "1112223333", CreatedAt = DateTime.UtcNow };
        var userService = new UserService(_userRepositoryMock.Object);

        // Act
        userService.CreateUser(newUser);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Create(newUser), Times.Once);
    }

    [Fact]
    public void DeleteUser_CallsRepositoryDelete()
    {
        // Arrange
        var userId = "1";
        var userService = new UserService(_userRepositoryMock.Object);

        // Act
        userService.DeleteUser(userId);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Delete(userId), Times.Once);
    }
}
