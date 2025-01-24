using FitTrack.BL.Services;
using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;
using Moq;

public class SubscriptionServiceUnitTest
{
    private readonly Mock<ISubscriptionRepository> _subscriptionRepositoryMock;
    private readonly List<Subscription> _subscriptions;

    public SubscriptionServiceUnitTest()
    {
        _subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
        _subscriptions = new List<Subscription>
        {
            new Subscription { Id = "1", UserId = "100", Type = "Premium", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), IsActive = true },
            new Subscription { Id = "2", UserId = "101", Type = "Basic", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), IsActive = true }
        };
    }

    [Fact]
    public void GetAllSubscriptions_ReturnsSubscriptionList()
    {
        // Arrange
        _subscriptionRepositoryMock.Setup(repo => repo.GetAll()).Returns(_subscriptions);
        var subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object);

        // Act
        var result = subscriptionService.GetAllSubscriptions();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_subscriptions.Count, result.Count());
    }

    [Fact]
    public void GetSubscriptionById_ReturnsSubscription()
    {
        // Arrange
        var subscriptionId = "1";
        _subscriptionRepositoryMock.Setup(repo => repo.GetById(subscriptionId)).Returns(_subscriptions.First(s => s.Id == subscriptionId));
        var subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object);

        // Act
        var result = subscriptionService.GetSubscriptionById(subscriptionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(subscriptionId, result.Id);
    }

    [Fact]
    public void CreateSubscription_CallsRepositoryCreate()
    {
        // Arrange
        var newSubscription = new Subscription { Id = "3", UserId = "102", Type = "Gold", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), IsActive = true };
        var subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object);

        // Act
        subscriptionService.CreateSubscription(newSubscription);

        // Assert
        _subscriptionRepositoryMock.Verify(repo => repo.Create(newSubscription), Times.Once);
    }

    [Fact]
    public void DeleteSubscription_CallsRepositoryDelete()
    {
        // Arrange
        var subscriptionId = "1";
        var subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object);

        // Act
        subscriptionService.DeleteSubscription(subscriptionId);

        // Assert
        _subscriptionRepositoryMock.Verify(repo => repo.Delete(subscriptionId), Times.Once);
    }
}

