using System;
using Hangfire;
using HangfireOne.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HangfireOne
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
            // resolving dependencies
            services.AddTransient<IInjectedInterface, JobPerformer>();

            // hangfire
            services.AddHangfire(
                config =>
                {
                    config.UseSqlServerStorage(Configuration.GetConnectionString("HangFireOne"));
                    // console colorido
                    config.UseColouredConsoleLogProvider();
                    // adição de um filtro de tentativas automáticas.
                    config.UseFilter(new AutomaticRetryAttribute { Attempts = 5 });
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var backgroundJobServerOptions = new BackgroundJobServerOptions()
            {
                WorkerCount = Environment.ProcessorCount * 5,
                ServerName = $"{Environment.MachineName}.{Guid.NewGuid().ToString()}",
                Queues = new[] { "critical", "default", "contingency" }
            };

            app.UseHangfireServer(options: backgroundJobServerOptions);
            app.UseHangfireDashboard();

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

            app.UseMvc();
        }
    }
}
