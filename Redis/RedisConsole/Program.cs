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
            using (var client = RedisBase.RedisClient)
            {
                client.StoreAsHash(people);
                var a = client.GetFromHash<People>("a");
            }

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
