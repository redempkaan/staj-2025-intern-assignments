using Microsoft.EntityFrameworkCore;
using VBT_proje.Models;

namespace VBT_proje.Data
{
    // EF Core's basic context class that represents the database
    public class AppDbContext : DbContext
    {
        // Constructor that takes database connection settings
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Definition of the Users table
        public DbSet<User> Users { get; set; }
    }
}