using Microsoft.Extensions.DependencyInjection;
using FitTrack.BL.Interfaces;
using FitTrack.BL.Services; 

namespace FitTrack.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddBusinessDependencies(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>(); 
            services.AddScoped<ISubscriptionService, SubscriptionService>();


            return services;
        }
    }
}

