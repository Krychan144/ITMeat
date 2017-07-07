using System;
using System.Linq;
using AngleSharp;
using ITMeat.BusinessLogic.Configuration.Implementations;
using ITMeat.BusinessLogic.Configuration.Interfaces;
using ITMeat.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ITMeat.WEB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework().AddDbContext<ITMeatDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddSession();

            RegisterDependecy.Register(services);

            AutoMapperBuilder.Build();

            services.Configure<EmailServiceCredentials>(Configuration.GetSection("EmailServiceCredentials"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMigrationHelper migrationHelper)
        {
            var debugValue = Configuration.GetSection("Logging:Loglevel:Default").Value;
            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), debugValue);

            //I'm gonna leave it as string array becase we might want to add some log modules later
            string[] logOnlyThese = { }; // or reverse string[] dontlong = {"ObjectResultExecutor", "JsonResultExecutor"};

            loggerFactory.AddDebug((category, _logLevel) => !logOnlyThese.Any(category.Contains) && _logLevel >= logLevel);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new PathString("/Auth/Login"),
                AccessDeniedPath = new PathString("/Home/AccessDenied"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            migrationHelper.Migrate();
        }
    }
}