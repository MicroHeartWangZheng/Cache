using Cache.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cache.Memory
{
    public class MemoryCacheManager : ICacheManager
    {
        MemoryCache memoryCache;

        public MemoryCacheManager(IOptions<MemoryCacheOptions> options)
        {
            memoryCache = new MemoryCache(options);
        }
        public void Add<T>(string key, T data, int cacheTime)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public string Test()
        {
            return "abc";
        }
    }
}
