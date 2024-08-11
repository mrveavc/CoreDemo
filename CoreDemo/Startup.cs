using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApiDemo.DataAccessLayer;
using CoreDemo.Controllers;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreDemo
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
            services.AddDbContext<Context>();
            services.AddIdentity<AppUser,AppRole>(x=>
            {
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<Context>();

            services.AddControllersWithViews();
            services.AddControllersWithViews();

           // services.AddSession(); // oturum i�in

			services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()  // Projenin t�m�ne yetkilendirme verdi�i i�in hi�bir sayfa a��lmaz.
															   // [AllowAnonymous] ile kurallardan muaf edebiliriz (LoginController)
															   // Oturum a��lmay�nca t�m urllerde logine y�nlendirme yap�l�yor. 
						   .RequireAuthenticatedUser()
                           .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                
            }); 

			services.AddMvc();
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme
                ).AddCookie(x =>
                {
                    x.LoginPath = "/Login/Index";
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(100); // 100 dk oturum a��k kalma s�resi
                options.LoginPath = "/Login/Index";
                options.SlidingExpiration = true;
            });
		}

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

            //app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            //app.UseSession(); // oturum i�in
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

               endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
