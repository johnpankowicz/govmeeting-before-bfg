using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Web_App
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

            string ClientfilesPath = @"C:\GOVMEETING\_SOURCECODE\FrontEnd\ClientApp\dist\ClientApp";
            //string ClientfilesPath = @"..\..\FrontEnd\ClientApp\dist\ClientApp";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    ClientfilesPath),
                RequestPath = "/client"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller}/{action}");
            //    routes.MapRoute("Spa", "{*url}", defaults: new { controller = "Home", action = "Spa" });
            //});

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
