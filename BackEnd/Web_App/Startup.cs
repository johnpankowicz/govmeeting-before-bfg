using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.WebApp.StartupCustomizations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace GM.WebApp
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
            services.AddControllersWithViews();

            // This enables the use of "Feature Folders".
            // https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // This sends all unhandled URLs to the static index.html page..
            // The default HomeController/Index is already configured to send index.html.
            // Can we remove the code below and handle this case in app.UseEndpoints?
            app.Use(async (context, next) =>
            {
                context.Request.Path = "/index.html";
                // I got a warning from FxCop to use ConfigureAwait instead of await:
                // "Calling ConfigureAwait(true) on the task has the same behavior as not explicitly 
                // calling ConfigureAwait. By explicitly calling this method, you're letting readers 
                // know you intentionally want to perform the continuation on the original synchronization context."
                // await next();
                await next().ConfigureAwait(true);
            });

            app.UseStaticFiles();

        }
    }
}
