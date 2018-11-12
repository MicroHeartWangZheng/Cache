using System;

namespace Cache.Infrastructure
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取一个指定的缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 缓存指定的对象
        /// </summary>
        /// <param name="key">缓存键名</param>
        /// <param name="data">需要要缓存的对象</param>
        /// <param name="cacheTime">缓存时间</param>
        void Add<T>(string key, T data, int cacheTime);

        /// <summary>
        /// 判断指定的对象是否已存在于缓存之中
        /// </summary>
        /// <param name="key">缓存键名</param>
        bool IsExist(string key);

        /// <summary>
        /// 删除指定的缓存对象
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// 删除符合规则的所有缓存对象
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清除所有缓存对象
        /// </summary>
        void Clear();
    }
}
