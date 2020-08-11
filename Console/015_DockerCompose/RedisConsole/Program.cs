using System;
using StackExchange.Redis;

namespace RedisConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AccessRedis();

            Console.WriteLine("Finish!!!");
        }

        private static void AccessRedis()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis");
            IDatabase db = redis.GetDatabase();
            db.StringSet("KeyDateTime", DateTime.Now.ToLongTimeString());

            var value = db.StringGetAsync("KeyDateTime").Result;

            Console.WriteLine($"Value: {value}");
        }
    }
}