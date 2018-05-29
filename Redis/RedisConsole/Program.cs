using ServiceStack.Redis;
using System;

namespace RedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            People people = new People()
            {
                Age = 18,
                Id = "a",
                Name = "Test"
            };

            var prcm = new PooledRedisClientManager(new string[] { "121.41.55.42:6379" }, new string[] { "121.41.55.42:6379" },
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = 20,
                                 MaxReadPoolSize = 20,
                                 AutoStart = true
                             });

            var client = prcm.GetClient();

            client.StoreAsHash(people);
            var a = client.GetFromHash<People>("a");

            Console.WriteLine("Hello World!");
        }
    }
    class People
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
