using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT")}");

            var appSettings = ReadFromAppSettings().Get<AppSettingsModel>();

            Console.WriteLine($"Redis Conntect: {appSettings.redis_connect}");

            AccessRedis(appSettings);

            Console.WriteLine("Finish!!!");
        }

        public static IConfigurationRoot ReadFromAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT")}.json", true)
                .Build();
        }

        private static void AccessRedis(AppSettingsModel appSettings)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(appSettings.redis_connect);
            IDatabase db = redis.GetDatabase();
            db.StringSet("KeyDateTime", DateTime.Now.ToLongTimeString());

            var value = db.StringGetAsync("KeyDateTime").Result;

            Console.WriteLine($"Value: {value}");
        }
    }
}