using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cache.Tools
{
    public class SerializationService : ISerializationService
    {

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
        };

        /// <inheritdoc />
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, typeof(T), Settings);
        }
        /// <inheritdoc />
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}
