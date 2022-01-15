
using ConnectionModels;
using Hash;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using Repositories.Interfaces;
using Repositories.Repositories;
using Serilog;
using Servi�es.Interfeces;
using Servi�es.Services;
using UI.MapingProfile;

namespace AdsProject
{
    public class Startup
    {
        private IConfiguration _�onfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            _�onfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_�onfiguration, "Serilog")
                .CreateLogger();
           
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddCors();

            services.Configure<AzureSettings>(_�onfiguration.GetSection("AzureSettings"));
            services.Configure<DBConfig>(_�onfiguration.GetSection("DBConfig"));
            services.Configure<HashConfig>(_�onfiguration.GetSection("HashConfig"));

            services.AddSingleton(Log.Logger);
            services.AddSingleton<IHasher, Hasher>();

            services.AddTransient<IAdsRepository, AdsRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IImageService, AzureImageService>();
            services.AddTransient<IAdsService, AdsService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error", "?status={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ForAds",
                    pattern: "/",
                    defaults:new {controller = "Ads", action = "GetAds" });
                endpoints.MapControllerRoute(
                    name: "ForException",
                    pattern: "/Error",
                    defaults: new { controller = "Error", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
