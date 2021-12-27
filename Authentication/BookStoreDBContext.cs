using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> context) : base(context)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
