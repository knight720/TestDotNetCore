using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace WebApplication1
{
    public class Program
    {
        private static Random _random = new Random();

        public static void Main(string[] args)
        {
            Program.CreateData();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void CreateData()
        {
            var row = 1000;
            var column = 1000;
            var fileName = "data.txt";
            if (File.Exists(fileName) == false)
            {
                var sw = File.CreateText(fileName);
                for(int i = 0; i < row; i++)
                {
                    sw.WriteLine(Program.CreateText(column));
                }
                sw.Close();
            }
        }

        private static string CreateText(int column)
        {
            var source = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var length = source.Length;

            var sb = new StringBuilder();
            for(int i = 0; i < column; i++)
            {
                var index = Program._random.Next(length);
                sb.Append(source[index]);
            }

            return sb.ToString();
        }
    }
}
