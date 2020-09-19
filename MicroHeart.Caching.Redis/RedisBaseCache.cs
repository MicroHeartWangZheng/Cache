using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroHeart.Caching.Redis
{
    public abstract class RedisBaseCache
    {
        protected IDatabase Cache;

        private volatile ConnectionMultiplexer _connection;
        private readonly ConfigurationOptions option;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);

        public RedisBaseCache(ConfigurationOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            this.option = options;
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
