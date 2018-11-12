using Cache.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Cache.Memory
{
    public class MemoryCacheManager : ICacheManager
    {
        MemoryCache MemoryCache;

        public MemoryCacheManager(IOptions<MemoryCacheOptions> options)
        {
            MemoryCache = new MemoryCache(options);
        }

        public void Add<T>(string key, T data, int cacheTime)
        {
            if (data == null)
                return;
            if (IsExist(key))
                throw new Exception($@"key:{key}已经存在");
            else
            {
                MemoryCache.Set(key, data, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(0, 0, cacheTime)
                });
            }

        }

        public void Clear()
        {
            MemoryCache.Compact(1d);//删除所有
        }

        public T Get<T>(string key)
        {
            return MemoryCache.Get<T>(key);
        }

        public bool IsExist(string key)
        {
            return MemoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            MemoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in GetCacheKeys())
                if (regex.IsMatch(item))
                    keysToRemove.Add(item);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        private List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = MemoryCache.GetType().GetField("_entries", flags).GetValue(MemoryCache);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }
            return keys;
        }
    }
}
