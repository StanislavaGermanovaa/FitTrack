using FitTrack.Models.Configurations;

namespace FitTrack.ServiceExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
     
            services.Configure<MongoDbConfiguration>(config.GetSection(nameof(MongoDbConfiguration)));

            return services;
        }
    }
}
