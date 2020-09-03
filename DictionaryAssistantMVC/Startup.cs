using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DictionaryAssistantMVC.Contexts;
using DictionaryAssistantMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DictionaryAssistantMVC
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* Database Stuff */
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DictionaryContext>(options => options.UseNpgsql(connectionString));

            /* CORS Policy */
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    string[] corsOrigins = this.Configuration.GetSection("CorsOrigins").GetChildren().Select(cs => cs.Value).ToArray();
                    builder.WithOrigins(corsOrigins);
                });
            });

            /* Required for Controllers */
            services.AddControllersWithViews();

            /* Services for this Application */
            services.AddTransient<IDictionaryService, DictionaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
