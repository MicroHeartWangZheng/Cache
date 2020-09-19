using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public class RedisStringCache : RedisBaseCache, IDistributedCache
    {
        public RedisStringCache(IOptions<ConfigurationOptions> optionsAccessor) : base(optionsAccessor)
        {
        }
        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Connect();
            var expiry = GetAbsoluteExpiration(DateTimeOffset.UtcNow, options);
            Cache.StringSet(key, value, expiry);
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            await ConnectAsync();
            var expiry = GetAbsoluteExpiration(DateTimeOffset.UtcNow, options);
            await Cache.StringSetAsync(key, value, expiry);
        }

        public byte[] Get(string key)
        {
            Connect();
            return Cache.StringGet(key);
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            await ConnectAsync();
            return await Cache.StringGetAsync(key);
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            Connect();
            Cache.KeyDelete(key);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            await ConnectAsync();
            await Cache.KeyDeleteAsync(key);
        }

        private static TimeSpan? GetAbsoluteExpiration(DateTimeOffset creationTime, DistributedCacheEntryOptions options)
        {
            if (options.AbsoluteExpiration.HasValue && options.AbsoluteExpiration <= creationTime)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(DistributedCacheEntryOptions.AbsoluteExpiration),
                    options.AbsoluteExpiration.Value,
                    "The absolute expiration value must be in the future.");
            }
            TimeSpan? absoluteExpiration = null;
            if (options.AbsoluteExpiration.HasValue)
                absoluteExpiration = options.AbsoluteExpiration - creationTime;
            if (options.AbsoluteExpirationRelativeToNow.HasValue)
                absoluteExpiration = options.AbsoluteExpirationRelativeToNow;
            return absoluteExpiration;
        }
    }
}
