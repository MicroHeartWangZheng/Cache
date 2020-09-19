using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHeart.Caching.Redis
{
    public static class RedisHashCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, string connStringName)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (string.IsNullOrEmpty(connStringName))
                throw new ArgumentNullException(nameof(connStringName));

            services.AddSingleton<ConfigurationOptions>(x =>
            {
                var configuration = x.GetService<IConfiguration>();
                return ConfigurationOptions.Parse(configuration.GetValue<string>(connStringName));
            });
            services.AddSingleton<IRedisCache, RedisCache>();
            return services;
        }
    }
}
