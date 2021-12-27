using BookStore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Data.Extensions
{
    public static class ServicesInjection
    {
        public static void AddProjectModules(this IServiceCollection services)
        {
            services.AddDbContext<BookStoreDBContext>(context => { context.UseInMemoryDatabase("BookStoreDb"); });
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
