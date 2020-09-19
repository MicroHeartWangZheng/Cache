using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHeart.Caching.Redis
{
    public static class RedisHashCacheExtensions
    {
        public void Set(string key, object obj, DistributedCacheEntryOptions options)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Connect();
            var expiry = GetAbsoluteExpiration(DateTimeOffset.UtcNow, options);
            Cache.HashSet(key, "field", value);
        }
    }
}
