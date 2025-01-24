using FitTrack.BL.Interfaces;
using FitTrack.BL.Services;
using FitTrack.Models.DTO;
using Moq;

public class BlUserServiceUnitTest
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ISubscriptionService> _subscriptionServiceMock;
    private readonly List<User> _users;
    private readonly List<Subscription> _subscriptions;

    public BlUserServiceUnitTest()
    {
        _userServiceMock = new Mock<IUserService>();
        _subscriptionServiceMock = new Mock<ISubscriptionService>();

        _users = new List<User>
        {
            new User { Id = "100", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", CreatedAt = DateTime.UtcNow },
            new User { Id = "101", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "0987654321", CreatedAt = DateTime.UtcNow }
        };

        _subscriptions = new List<Subscription>
        {
            new Subscription { Id = "1", UserId = "100", Type = "Premium", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), IsActive = true },
            new Subscription { Id = "2", UserId = "101", Type = "Basic", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), IsActive = true }
        };
    }

    [Fact]
    public void GetUserWithSubscriptions_ReturnsSubscriptionsForUser()
    {
        // Arrange
        var userId = "100";
        _userServiceMock.Setup(service => service.GetUserById(userId)).Returns(_users.First(u => u.Id == userId));
        _subscriptionServiceMock.Setup(service => service.GetAllSubscriptions()).Returns(_subscriptions);
        var blUserService = new BlUserService(_userServiceMock.Object, _subscriptionServiceMock.Object);

        // Act
        var result = blUserService.GetUserWithSubscriptions(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(userId, result.First().UserId);
    }

    [Fact]
    public void GetUserWithSubscriptions_ReturnsEmptyList_WhenUserNotFound()
    {
        // Arrange
        var userId = "999";
        _userServiceMock.Setup(service => service.GetUserById(userId)).Returns((User)null);
        var blUserService = new BlUserService(_userServiceMock.Object, _subscriptionServiceMock.Object);

        // Act
        var result = blUserService.GetUserWithSubscriptions(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
