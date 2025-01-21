using Microsoft.Extensions.DependencyInjection;
using FitTrack.DL.Interfaces;
using FitTrack.DL.Repositories;

namespace FitTrack.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddDataDependencies(this IServiceCollection services)
        {

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();

            return services;
        }
    }
}
