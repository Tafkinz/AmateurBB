using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Factories;
using BL.Services;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using DAL.App.Interfaces;
using DAL.EF;
using DAL.interfaces;
using DAL.Interfaces.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WebApp"));
            });
        

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 8;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IRepositoryFactoryProvider, EFRepositoryFactoryProvider>();
            services.AddScoped<IRepositoryProvider, EFRepositoryProvider>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IDataContext, ApplicationDbContext>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamFactory, TeamFactory>();
            services.AddScoped<ICourtFactory, CourtFactory>();
            services.AddScoped<IStandingFactory, StandingFactory>();
            services.AddScoped<IGameResultFactory, GameResultFactory>();

            //XML and JSON
            services.AddMvc(options => { options.RespectBrowserAcceptHeader = true; });
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
