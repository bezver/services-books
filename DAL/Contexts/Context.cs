using Microsoft.EntityFrameworkCore;
using Books.Domain.Models;

namespace Books.DAL.Contexts
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
    }
}
