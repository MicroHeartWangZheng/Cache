using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConsole
{
    public class RedisManager
    {
        public static RedisConfigOption RedisConfigOption;

        private static PooledRedisClientManager pooledRedisClientManager;

        static RedisManager()
        {
            RedisConfigOption = new RedisConfigOption()
            {
                AutoStart = true,
                ReadOnlyHosts = new string[] { "127.0.0.1:6379" },
                ReadWriteHosts = new string[] { "127.0.0.1:6379" },
                MaxReadPoolSize = 1,
                MaxWritePoolSize = 1
            };
            CreateManager();
        }

        private static void CreateManager()
        {
            pooledRedisClientManager = new PooledRedisClientManager(RedisConfigOption.ReadWriteHosts, RedisConfigOption.ReadOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = RedisConfigOption.MaxWritePoolSize,
                MaxReadPoolSize = RedisConfigOption.MaxReadPoolSize,
                AutoStart = RedisConfigOption.AutoStart,
            });
        }

        public static IRedisClient GetClient()
        {
            if (pooledRedisClientManager == null)
                CreateManager();
            return pooledRedisClientManager.GetClient();
        }
    }
}
