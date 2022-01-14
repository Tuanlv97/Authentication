using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> context) : base(context)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
