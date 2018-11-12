using Cache.Infrastructure;
using Cache.Tools;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cache.Redis
{
    public class RedisManager : ICacheManager
    {
        private readonly ISerializationService _serializer;

        private readonly RedisKeyOptions RedisKeyOptions;

        public RedisManager(IOptions<ConfigurationOptions> options,
            IOptions<RedisKeyOptions> redisKeyOptions,
            ISerializationService serializer)
        {
            RedisHelper.ConfigurationOptions = options.Value;
            RedisKeyOptions = redisKeyOptions.Value;
        }

        public void Add<T>(string key, T data, int cacheTime)
        {
            RedisHelper.Execute((database) =>
            {
                if (database == null)
                    return;
                var json = _serializer.Serialize(data);
                database.HashSet(RedisKeyOptions.RedisKey, key, json);
                database.KeyExpire(RedisKeyOptions.RedisKey, new TimeSpan(0, 0, cacheTime));
            });
        }

        public void Clear()
        {
            RedisHelper.Execute((database) =>
            {
                if (database == null)
                    return;
                database.KeyDelete(RedisKeyOptions.RedisKey);
            });
        }

        public T Get<T>(string key)
        {
            var data = string.Empty;
            data = RedisHelper.Execute((database) =>
            {
                if (database == null || !IsExist(key))
                    return string.Empty;
                return database.HashGet(RedisKeyOptions.RedisKey, key).ToString();
            });
            if (string.IsNullOrWhiteSpace(data))
                return default(T);
            var result = _serializer.Deserialize<T>(data);
            return result;
        }

        public bool IsExist(string key)
        {
            return RedisHelper.Execute((database) =>
            {
                if (database == null)
                    return false;
                return database.HashExists(RedisKeyOptions.RedisKey, key);
            });
        }

        public void Remove(string key)
        {
            RedisHelper.Execute((database) =>
            {
                if (database == null)
                    return;
                database.HashDelete(RedisKeyOptions.RedisKey, key);
            });
        }

        public void RemoveByPattern(string pattern)
        {
            RedisValue[] rvs = RedisHelper.Execute((database) =>
            {
                if (database == null)
                    return null;
                return database.HashKeys(RedisKeyOptions.RedisKey);
            });

            var keys = rvs.Where(m => Regex.IsMatch(m, pattern)).ToList();

            foreach (var key in keys)
            {
                RedisHelper.Execute((database) =>
                {
                    database.HashDelete(RedisKeyOptions.RedisKey, key);
                });
            }
        }
    }
}
