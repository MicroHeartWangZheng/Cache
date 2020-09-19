using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public interface IRedisCache
    {
        T Execute<T>(Func<IDatabase, T> func);

        Task<T> ExecuteAsync<T>(Func<IDatabase, Task<T>> func);
    }
}
