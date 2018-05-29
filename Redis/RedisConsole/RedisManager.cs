using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConsole
{
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfigOption RedisConfigOption { get; set; }

        private static PooledRedisClientManager pooledRedisClientManager;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            pooledRedisClientManager = new PooledRedisClientManager(RedisConfigOption.ReadWriteHosts, RedisConfigOption.ReadOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = RedisConfigOption.MaxWritePoolSize,
                MaxReadPoolSize = RedisConfigOption.MaxReadPoolSize,
                AutoStart = RedisConfigOption.AutoStart,
            });
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (pooledRedisClientManager == null)
                CreateManager();
            return pooledRedisClientManager.GetClient();
        }
    }
}
