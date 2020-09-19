using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public abstract class RedisBaseCache
    {
        private volatile ConnectionMultiplexer _connection;
        protected IDatabase Cache;
        private readonly ConfigurationOptions option;

        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        public RedisBaseCache(IOptions<ConfigurationOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
                throw new ArgumentNullException(nameof(optionsAccessor));
            option = optionsAccessor.Value;
        }

        protected void Connect()
        {
            if (Cache != null)
                return;
            _connectionLock.Wait();
            try
            {
                if (Cache == null)
                {
                    if (option != null)
                        _connection = ConnectionMultiplexer.Connect(option);
                    Cache = _connection.GetDatabase();
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        protected async Task ConnectAsync(CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            if (Cache != null)
                return;

            await _connectionLock.WaitAsync(token);
            try
            {
                if (Cache == null)
                {
                    if (option != null)
                        _connection = await ConnectionMultiplexer.ConnectAsync(option);
                    Cache = _connection.GetDatabase();
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }
}
