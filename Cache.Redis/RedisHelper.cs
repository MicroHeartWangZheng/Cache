using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cache.Redis
{
    public static class RedisHelper
    {
        /// <summary>
        /// 连接配置
        /// </summary>
        internal static ConfigurationOptions ConfigurationOptions;

        private static ConnectionMultiplexer ConnectionMultiplexer;

        /// <summary>
        /// 连接redis并执行命令
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action<IDatabase> action)
        {
            ExecuteInConnection(action);
        }

        /// <summary>
        /// 连接redis并执行命令
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action<IDatabase, IServer> action)
        {
            ExecuteInConnection(action);
        }

        /// <summary>
        /// 连接redis并执行命令和返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Execute<T>(Func<IDatabase, T> action)
        {
            return ExecuteInConnection(action);
        }

        /// <summary>
        /// 连接redis并执行命令和返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Execute<T>(Func<IDatabase, IServer, T> action)
        {
            return ExecuteInConnection(action);
        }

        static void ExecuteInConnection(Action<IDatabase> action)
        {
            if (action == null)
                return;
            var connection = Connect();
            var database = connection.GetDatabase();
            action?.Invoke(database);
        }
        static void ExecuteInConnection(Action<IDatabase, IServer> action)
        {
            if (action == null)
                return;
            var connection = Connect();
            var database = connection.GetDatabase();
            var redisServer = connection.GetServer(connection.GetEndPoints().First());
            action?.Invoke(database, redisServer);
        }
        static T ExecuteInConnection<T>(Func<IDatabase, T> action)
        {
            if (action == null)
                return default(T);

            var connection = Connect();
            var database = connection.GetDatabase();
            return action.Invoke(database);
        }
        static T ExecuteInConnection<T>(Func<IDatabase, IServer, T> action)
        {
            if (action == null)
                return default(T);
            var connection = Connect();
            var database = connection.GetDatabase();
            var redisServer = connection.GetServer(connection.GetEndPoints().First());
            return action.Invoke(database, redisServer);
        }
        static ConnectionMultiplexer Connect()
        {
            if (ConnectionMultiplexer != null)
                return ConnectionMultiplexer;

            ConnectionMultiplexer = ConnectionMultiplexer.Connect(ConfigurationOptions);
            return ConnectionMultiplexer;
        }
    }
}
