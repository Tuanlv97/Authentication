using BookStore.WebApplication.Services;
using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test
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
            var secret = "secret".ToSha256();
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
            .AddOpenIdConnect("oidc", o =>
            {
                o.SignInScheme = "Cookies";
                o.Authority = "https://localhost:5000";
                o.RequireHttpsMetadata = false;
                o.GetClaimsFromUserInfoEndpoint = true;
                o.ClientId = "bookstore_webapp";
                o.ClientSecret = "secret";
                o.Scope.Add("openid");
                o.Scope.Add("profile");
                o.Scope.Add("bookstore");
                o.Scope.Add("bookstore_apis");
                o.Scope.Add("bookstore_viewbook");
                o.Scope.Add("bookstore");
                o.ResponseType = "code";
                o.SaveTokens = true;
            });

            services.AddHttpClient<IBookStoreAPIService, BookStoreAPIService>();
            services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}