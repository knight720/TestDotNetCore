using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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

            services.AddScoped<CustomService, CustomService>();
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

                //// 檔案清單
                app.UseFileServer(new FileServerOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.ContentRootPath, @"bin")
                    ),
                    RequestPath = new PathString("/StaticFiles"),
                    EnableDirectoryBrowsing = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //// 預設檔案 - 嘗試請求預設檔案
            //// default.htm, default.html, index.htm, index.html
            //app.UseDefaultFiles(); //// 會覆蓋專案預設的頁面
            //// 自訂預設檔案
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Add("custom.html");
            //app.UseDefaultFiles(defaultFilesOptions);

            //// 啟用靜態檔案 - 回傳請求的檔案
            app.UseStaticFiles();
            //// 啟用指定目錄
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, @"node_modules")),
                RequestPath = new PathString("/third-party")
            });

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