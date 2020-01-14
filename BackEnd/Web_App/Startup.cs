using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Google;

using GM.Configuration;
using GM.WebApp.StartupCustomizations;
using GM.DatabaseRepositories;
using GM.DatabaseAccess;
using GM.FileDataRepositories;
//using GM.WebApp.Services;



namespace GM.WebApp
{
    public class Startup
    {
        readonly NLog.Logger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            /* Logging will be written to the "logs" folder two levels up from the application base path.
             *   Two files will be written to:
             *   "nlog.own.<date>.log" - contains our application log messages only
             *   "nlog.all.<date>.log  - contains our messages and system messages  
             */
            _logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set a variable in the gdc which is be used in NLog.config for the
            // base path of our app: ${gdc:item=appbasepath} 
            var appBasePath = System.IO.Directory.GetCurrentDirectory();
            GlobalDiagnosticsContext.Set("appbasepath", appBasePath);

            _logger.Trace("GM: In ConfigureServices");

            _logger.Info("GM: Add AppSettings");
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AppSettings>(myOptions =>
            {
                // Modify the DataFilesPath option to be the full path.
                //myOptions.DatafilesPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), myOptions.DatafilesPath);
                myOptions.DatafilesPath = GMFileAccess.GetFullPath(myOptions.DatafilesPath);
                myOptions.TestfilesPath = GMFileAccess.GetFullPath(myOptions.TestfilesPath);
                Console.WriteLine("Datafile path = " + myOptions.DatafilesPath);
            });

            _logger.Trace("GM: Add ApplicationDbContext");
            // We will be able to access ApplicationDbContext in a controller with:
            //    public MyController(ApplicationDbContext context) { ... }
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            _logger.Trace("GM: Add Add Authentication");
            // ConfigureAuthenticationServices(services);
            ConfigureAuthentication.ConfigureAuthenticationServices(services, Configuration, _logger);

            services.AddControllersWithViews();

            _logger.Trace("GM: Enable use of Feature Folders");
            // https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });

            _logger.Trace("GM: Exit ConfigureServices");
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

        private void ConfigureAuthenticationServices(IServiceCollection services)
        {
            services.AddAuthentication()
            .AddGoogle(options => {
                options.ClientId = Configuration["ExternalAuth:Google:ClientId"];
                options.ClientSecret = Configuration["ExternalAuth:Google:ClientSecret"];
            });

            _logger.Trace("GM: Add Identity");

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Govmeeting: Set options for cookie expiration.
                // TODO - While upgrading to .NET SDK 2.0, I was getting an error on the next two line so
                // I commented them out. Error = "IdentityOptions does not contain a definition for Cookies"
                //options.Cookies.ApplicationCookie.SlidingExpiration = true;
                //options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // amount of time they are locked out
                options.Lockout.AllowedForNewUsers = true;
                // TODO We should send the admin an email if someone is locked out.
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Govmeeting: Brock Allen suggest stronger hashing instead of the default.
            //services.Configure<PasswordHasherOptions>(options =>
            //{
            //    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2;
            //    options.IterationCount = 20000;
            //});

            _logger.Trace("GM: Add Authorization");

            // https://docs.asp.net/en/latest/security/authorization/claims.html
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                { policy.RequireClaim("role", "administrator"); });

                options.AddPolicy("Editor", policy =>
                { policy.RequireClaim("role", "editor"); });

                options.AddPolicy("Proofreader", policy =>
                { policy.RequireClaim("role", "proofreader"); });

                options.AddPolicy("PhillyEditor", policy =>
                {
                    policy.RequireClaim("role", "editor");
                    policy.RequireClaim("location", "Philadelphia");
                });
                options.AddPolicy("BbhEditor", policy =>
                {
                    policy.RequireClaim("role", "editor");
                    policy.RequireClaim("location", "Boothbay Harbor");
                });
            });

        }


    }
}
