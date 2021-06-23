using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShmotStore.Models;

namespace ShmotStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(connection));
            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });
            services.AddControllersWithViews();
            services.AddScoped<AdminService>();
            services.AddScoped<HomeService>();
            services.AddScoped<CartService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseMvc(route =>
            {
                route.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");
                route.MapRoute(name: "defaultAdmin", template: "{controller=Admin}/{action=Data}/{Id?}");
                route.MapRoute(name: "cart", template: "{controller=Cart}/{action=Cart}/{Id?}");
            });
        }
    }
}
