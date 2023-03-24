using BookStore.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStore.API.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        {
            
        }
        public DbSet<Books> Books { get; set; }
    }
}
