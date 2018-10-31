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

            //// �C���`�J�ɡA�����s new �@�ӷs�����
            services.AddTransient<ISampleTransient, Sample>();
            //// �C�� Request �����s new �@�ӷs����ҡA�P�@�� Request ���޸g�L�h�֭� Pipeline ���O�ΦP�@�ӹ��
            services.AddScoped<ISampleScoped, Sample>();
            //// �Q��Ҥƫ�N���|�����A�{���B������u�|���@�ӹ��
            services.AddSingleton<ISampleSingleton, Sample>();
            // Singleton �]�i�H�ΥH�U��k���U
            // services.AddSingleton<ISampleSingleton>(new Sample());

            services.AddScoped<CustomService, CustomService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            //app.UseMiddleware<FirstMiddleware
            //����X�R��k
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

                //// �ɮײM��
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

            //// �w�]�ɮ� - ���սШD�w�]�ɮ�
            //// default.htm, default.html, index.htm, index.html
            //app.UseDefaultFiles(); //// �|�л\�M�׹w�]������
            //// �ۭq�w�]�ɮ�
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Add("custom.html");
            //app.UseDefaultFiles(defaultFilesOptions);

            //// �ҥ��R�A�ɮ� - �^�ǽШD���ɮ�
            app.UseStaticFiles();
            //// �ҥΫ��w�ؿ�
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