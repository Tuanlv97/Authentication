using BookStore.WebApplication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test
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
            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddHttpContextAccessor();
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect(o =>
                {
                    // o.Authority = "https://localhost:44380";
                    o.Authority = "https://localhost:5000";
                    o.ClientId = "bookstore_webapp";
                    o.ClientSecret = "supersecret";
                    o.CallbackPath = "/sign-oidc";

                    o.Scope.Add("openid");//
                    o.Scope.Add("bookstore");
                    o.Scope.Add("bookstore_apis");

                    o.SaveTokens = true;
                    o.GetClaimsFromUserInfoEndpoint = true;

                    ////o.ClaimActions.MapUniqueJsonKey("Address", "Address");

                    o.ResponseType = "code";
                    o.ResponseMode = "form_post";

                    o.UsePkce = true;
                });
            services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

            //services.AddHttpClient<IBookStoreAPIService, BookStoreAPIService>(
            //    async (c, client) =>
            //    {
            //        var accessor = c.GetRequiredService<IHttpContextAccessor>();
            //        var accessToken = await accessor.HttpContext.GetTokenAsync("access_token");
            //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            //        client.BaseAddress = new Uri("https://localhost:5009/api/");
            //    });
            services.AddHttpClient<IBookStoreAPIService, BookStoreAPIService>();
           
            //services.AddControllersWithViews();
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
