using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Exteisions;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //// 每次注入時，都重新 new 一個新的實例
            services.AddTransient<ISampleTransient, Sample>();
            //// 每個 Request 都重新 new 一個新的實例，同一個 Request 不管經過多少個 Pipeline 都是用同一個實例
            services.AddScoped<ISampleScoped, Sample>();
            //// 被實例化後就不會消失，程式運行期間只會有一個實例
            services.AddSingleton<ISampleSingleton, Sample>();
            // Singleton 也可以用以下方法註冊
            // services.AddSingleton<ISampleSingleton>(new Sample());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            //app.UseMiddleware<FirstMiddleware
            //改用擴充方法
            app.UseFirstMiddleware();

            app.Map("/second", mapApp =>
            {
                mapApp.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Second Middleware in. \r\n");
                    await next.Invoke();
                    await context.Response.WriteAsync("Second Middleware out. \r\n");
                });
                mapApp.Run(async context =>
                {
                    await context.Response.WriteAsync("Second. \r\n");
                });
            });

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            appLifetime.ApplicationStarted.Register(() =>
            {
                Program.Output("ApplicationLifetime - Started");
            });

            appLifetime.ApplicationStopping.Register(() =>
            {
                Program.Output("ApplicationLifetime - Stopping");
            });

            appLifetime.ApplicationStopped.Register(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("ApplicationLifetime - Stopped");
            });

        }
    }
}
