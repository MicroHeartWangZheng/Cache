using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public class RedisCache : RedisBaseCache, IRedisCache
    {
        public RedisCache(IOptions<ConfigurationOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public T Execute<T>(Func<IDatabase, T> func)
        {
            Connect();
            return func(Cache);
        }


        public async Task<T> Execute<T>(Func<IDatabase, Task<T>> func)
        {
            await ConnectAsync();
            return await func(Cache);
        }
    }
}
