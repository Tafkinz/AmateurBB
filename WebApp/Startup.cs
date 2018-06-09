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
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using BL.Util;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Filter;

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

            //JWT security
            services.AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;

                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["Token:Issuer"],
                            ValidAudience = Configuration["Token:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["Token:Key"])
                                )
                        };
                    });

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BasketBall API",
                    Description = "API for managing amateur basketball league games and results",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Taavi Kivimaa",
                        Email = "taavi.kivimaa@snowhound.eu"
                    },
                    License = new License
                    {
                        Name = "Use as you wish",
                        Url = "https://www.neti.ee"
                    }
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
            services.AddTransient<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddSingleton<IRepositoryFactoryProvider, EFRepositoryFactoryProvider>();
            services.AddScoped<IRepositoryProvider, EFRepositoryProvider>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IDataContext, ApplicationDbContext>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITeamFactory, TeamFactory>();
            services.AddScoped<ICourtFactory, CourtFactory>();
            services.AddScoped<IStandingFactory, StandingFactory>();
            services.AddScoped<IGameFactory, GameFactory>();
            services.AddScoped<IContactsFactory, ContactFactory>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IGameResultFactory, GameResultFactory>();
            services.AddScoped<AuthUtil>();

            //XML and JSON
            services.AddMvc(options => { options.RespectBrowserAcceptHeader = true; });
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc().AddJsonOptions(options =>
                        {
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                            options.SerializerSettings.Formatting = Formatting.Indented;
                        });

            //To make api calls from frontend
            services.AddCors();
            //Intercepting controller filter
            services.AddMvc(options =>
            {
                options.Filters.Add<ControllerSecurityFilter>();
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketBall API v1");
            });

            app.UseStaticFiles();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

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
