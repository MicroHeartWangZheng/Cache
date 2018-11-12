using System;

namespace Cache.Tools
{
    public interface ISerializationService
    {
        /// <summary>
        /// 将指定对象序列化为可传输的字节数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// 将指定的数据传输字节反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Deserialize<T>(string json);

    }
}
