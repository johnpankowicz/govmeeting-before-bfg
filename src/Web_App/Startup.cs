using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Identity;
using GM.WebApp.StartupCustomizations;
using GM.WebApp.Data;
using GM.WebApp.Services;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog;
using GM.FileDataRepositories;
using GM.DatabaseRepositories;
using GM.Configuration;

namespace GM.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            DebugStartup("In Startup");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ConfigureServices is called by the runtime. It adds services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // We will redirect console output to a file during production.
            services.AddSingleton<IRedirectConsole, RedirectConsole>();

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AppSettings>(myOptions =>
            {
                // Modify the DataFilesPath option to be the full path.
                //myOptions.DatafilesPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), myOptions.DatafilesPath);
                Console.WriteLine("Datafile path = " + myOptions.DatafilesPath);
            });

            ///////////////////////////////////////////////////////////////////////
            DebugStartup("In ConfigureServices - Configure Identity Services");
            ///////////////////////////////////////////////////////////////////////

            // We will be able to access ApplicationDbContext in a controller with:
            //    public MyController(ApplicationDbContext context) { ... }
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            DebugStartup("In ConfigureServices - after AddDbContext");
            DebugStartup("ConnectionString: " + Configuration["Data:DefaultConnection:ConnectionString"]);

            // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x
            services.AddAuthentication()
            .AddGoogle(options => {
                options.ClientId = Configuration["ExternalAuth:Google:ClientId"];
                options.ClientSecret = Configuration["ExternalAuth:Google:ClientSecret"];
            });

            ///////////////////////////////////////////////////////////////////////
            DebugStartup("In ConfigureServices - Configure Identity Services");
            ///////////////////////////////////////////////////////////////////////

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Govmeeting: Set options for cookie expiration.
                // Todo-g - While upgrading to .NET SDK 2.0, I was getting an error on the next two line so
                // I commented them out. Error = "IdentityOptions does not contain a definition for Cookies"
                //options.Cookies.ApplicationCookie.SlidingExpiration = true;
                //options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // amount of time they are locked out
                options.Lockout.AllowedForNewUsers = true;
                // Todo-g We should send the admin an email if someone is locked out.
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            DebugStartup("In ConfigureServices - after AddIdentity");

            // Govmeeting: Brock Allen suggest stronger hashing instead of the default.
            //services.Configure<PasswordHasherOptions>(options =>
            //{
            //    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2;
            //    options.IterationCount = 20000;
            //});

                        // https://docs.asp.net/en/latest/security/authorization/claims.html
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                {
                    policy.RequireClaim("role", "administrator");
                });

                options.AddPolicy("Editor", policy =>
                {
                    policy.RequireClaim("role", "editor");
                });

                options.AddPolicy("Proofreader", policy =>
                {
                    policy.RequireClaim("role", "proofreader");
                });

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

            DebugStartup("In ConfigureServices - after second AddAuthorization");

            // Add framework services.
            services.AddMvc()
                .AddXmlSerializerFormatters();

            DebugStartup("In ConfigureServices - after AddMvc");

            // This enables the use of "Feature Folders".
            // https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });


            // Add repositories
            services.AddSingleton<IGovBodyRepository, GovBodyRepositoryStub>();     // use stub
            services.AddSingleton<IMeetingRepository, MeetingRepositoryStub>();     // use stub
            services.AddSingleton<IViewMeetingRepository, ViewMeetingRepository>();
            services.AddSingleton<IAddtagsRepository, AddtagsRepository>();
            services.AddSingleton<IFixasrRepository, FixasrRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            DebugStartup("In ConfigureServices - after AddTransient");

            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddTransient<MeetingFolder>();

            services.AddSingleton(Configuration);

            services.AddScoped<ValidateReCaptchaAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IRedirectConsole redirect,
            IDbInitializer dbInitializer,
            ILoggerFactory loggerFactory,
            ApplicationDbContext db
            )
            //RoleManager<IdentityRole> roleManager,
        {
            // Logging configuration is now part of the "WebHost.CreateDefaultBuilder(args)" call in Program.cs
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            // Configure NLog
            //loggerFactory.AddNLog();        //add NLog to ASP.NET Core
            //app.AddNLogWeb();               //add NLog.Web
            ////needed for non-NETSTANDARD platforms: configure nlog.config in your project root. 
            //// NB: you need NLog.Web.AspNetCore package for this.         
            //env.ConfigureNLog("nlog.config");

            var appBasePath = System.IO.Directory.GetCurrentDirectory();
            GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
            var logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

            redirect.Start();
            Console.WriteLine("Startup.cs - Time = " + DateTime.Now);
            Console.WriteLine("Startup.cs - connection string = " + Configuration["Data:DefaultConnection:ConnectionString"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                // added for debugging deploy problem.
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();

                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                // Using migrations with asp.net core: https://dzone.com/articles/how-to-use-migration-with-entity-framework-core 
                try
                {
                    // This is from before we add ApplicationDbContext to Congigure() arguments:
                    //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    //    .CreateScope())
                    //{
                    //    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    //         .Database.Migrate();
                    //}
                    db.Database.Migrate();
                }
                catch { }
            }

            app.UseStaticFiles();

            DebugStartup("In Configure - after UseStaticFiles");

            // When the client code was in src\Client\BrowserApp, we created a FileProvider for that folder.
            //string browserApp = GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Client\BrowserApp"));
            //    app.UseStaticFiles(new StaticFileOptions()
            //    {
            //        FileProvider = new PhysicalFileProvider(browserAppPath),
            //        RequestPath = new PathString("/ba")
            //    });
            //}


            // Add a PhysicalFileProvider for the Datafiles folder. Until we have a way to serve video files to 
            // videogular via the API, we need to allow these to be accessed as static files.
            //string datafilesPath = Path.GetFullPath(Path.Combine(env.ContentRootPath, Configuration["TypedOptions:DatafilesPath"]));
            //datafilesPath = @"C:\ClientSites\govmeeting.org\Datafiles";
            string datafilesPath = Configuration["AppSettings:DatafilesPath"];
            datafilesPath = GetFullPath(datafilesPath);
            if (!Directory.Exists(datafilesPath))
            {
                Directory.CreateDirectory(datafilesPath);
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(datafilesPath),
                RequestPath = new PathString("/datafiles")
            });
            DebugStartup("In Configure - datafiles = " + datafilesPath);


            // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x
            app.UseAuthentication();

            DebugStartup("In Configure - after UseAuthentication");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                /* The following is for Angular SPA routes. When someone does a browser refresh or uses a 
                 * bookmark that's a deep link into the SPA, a request is sent to the server, instead
                 * of being handled by the SPA. The server does not find a controller for this route
                 * and returns a 404, Not Found. This map route redirects the request immediately to
                 * the index page of the Home controller. This returns the page containing the SPA. Once
                 * the SPA is running, it sees the URL that is being requested and handles it properly.
                 */
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            DebugStartup("In Configure - after UseMvc");

            // Create seed data
            dbInitializer.Initialize().Wait();

            DebugStartup("In Configure - after DbInitializer.Initialize");

        }

        // Modify the path to be the full path, if it is a relative path.
        string GetFullPath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }
            return Path.GetFullPath(path);
        }

        private void DebugStartup(string message)
        {
            File.AppendAllText("DebugLogStartup.txt", message + "\r\n");
        }
    }
}
