using FitTrack.Models.DTO;
using Mapster;
using FitTrack.Models.Response;

namespace FitTrack.MapsterConfig
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<User, UserResponse>.NewConfig();
            TypeAdapterConfig<Subscription, SubscriptionResponse>.NewConfig();

        }
    }
}
