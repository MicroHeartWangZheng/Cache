using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;

namespace Cache.Redis
{
    public class RedisManager : IDisposable
    {
        public readonly RedisConfigOptions RedisConfigOption;

        private PooledRedisClientManager pooledRedisClientManager;

        public IRedisClient RedisClient { get; set; }
        public RedisManager(RedisConfigOptions redisConfigOption)
        {
            RedisConfigOption = redisConfigOption;
            CreateManager();
        }

        private void CreateManager()
        {
            pooledRedisClientManager = new PooledRedisClientManager(RedisConfigOption.ReadWriteHosts, RedisConfigOption.ReadOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = RedisConfigOption.MaxWritePoolSize,
                MaxReadPoolSize = RedisConfigOption.MaxReadPoolSize,
                AutoStart = RedisConfigOption.AutoStart,
            });
        }

        public IRedisClient GetClient()
        {
            if (pooledRedisClientManager == null)
                CreateManager();

            if (RedisClient == null)
                return RedisClient = pooledRedisClientManager.GetClient();

            return RedisClient;
        }

        public void Dispose()
        {
            RedisClient.Dispose();
            RedisClient = null;
        }
    }
}
