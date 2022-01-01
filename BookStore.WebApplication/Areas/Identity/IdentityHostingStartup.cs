using System;
using BookStore.WebApplication.Clamins;
using BookStore.WebApplication.Data;
using BookStore.WebApplication.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BookStore.WebApplication.Areas.Identity.IdentityHostingStartup))]
namespace BookStore.WebApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BookStoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BookStoreContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<BookStoreContext>();
                services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsprincipalFactory>();
            });
        }
    }
}