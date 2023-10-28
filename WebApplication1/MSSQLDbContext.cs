using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MSSQLDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MSSQLDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
