using System.Runtime.Loader;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AssemblyLoadContext.Default.Unloading += Default_Unloading; ;

            CreateHostBuilder(args).Build().Run();
        }

        private static void Default_Unloading(AssemblyLoadContext obj)
        {
            WeatherForecastController.Unloading = true;
            Thread.Sleep(3000);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}