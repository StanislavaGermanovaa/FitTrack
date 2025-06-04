using Microsoft.Extensions.DependencyInjection;
using FitTrack.DL.Interfaces;
using FitTrack.DL.Repositories;
using FitTrack.DL.Cache;
using FitTrack.DL.Kafka;
using FitTrack.Models.DTO;
using Microsoft.Extensions.Configuration;
using FitTrack.Models.Configurations.CachePopulator;
using FitTrack.DL.Kafka.KafkaCache;


namespace FitTrack.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();

            // Добавяне на кеш конфигурация и producer-и
            services.AddCache<UserCacheConfiguration, UserRepository, User, string>(config);
            services.AddCache<SubscriptionCacheConfiguration, SubscriptionRepository, Subscription, string>(config);

            // Хостнати Kafka услуги (ако искаш да вървят паралелно)
            services.AddHostedService<KafkaCache<string, User>>();
            services.AddHostedService<KafkaCache<string, Subscription>>();

            return services;
        }

        /// <summary>
        /// Adds the cache configuration to the service collection.
        /// </summary>
        /// <typeparam name="TCacheConfiguration"></typeparam>
        /// <typeparam name="TCacheRepository"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>


        public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(this IServiceCollection services, IConfiguration config)
          where TCacheConfiguration : CacheConfiguration
          where TCacheRepository : class, ICacheRepository<TKey, TData>
          where TData : ICacheItem<TKey>
          where TKey : notnull
        {
            var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

            if (!configSection.Exists())
            {
                throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing in appsettings!");
            }

            services.Configure<TCacheConfiguration>(configSection);

            services.AddSingleton<ICacheRepository<TKey, TData>, TCacheRepository>();
            services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData, TCacheConfiguration>>();
            services.AddHostedService<MongoCachePopulator<TData, TCacheConfiguration, TKey>>();

            return services;
        }
    }
}
