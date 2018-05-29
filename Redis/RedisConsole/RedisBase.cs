using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConsole
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        public static IRedisClient RedisClient { get; private set; }
        private bool _disposed = false;
        static RedisBase()
        {
            RedisClient = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    RedisClient.Dispose();
                    RedisClient = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            RedisClient.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            RedisClient.SaveAsync();
        }
    }
}
