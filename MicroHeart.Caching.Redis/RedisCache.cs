using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public class RedisCache : RedisBaseCache, IRedisCache
    {
        public RedisCache(ConfigurationOptions options) : base(options)
        {
        }

        public T Execute<T>(Func<IDatabase, T> func)
        {
            Connect();
            return func(Cache);
        }

        public async Task<T> ExecuteAsync<T>(Func<IDatabase, Task<T>> func)
        {
            await ConnectAsync();
            return await func(Cache);
        }
    }
}
