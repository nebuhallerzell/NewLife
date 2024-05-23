using Microsoft.EntityFrameworkCore;
using NewLife.Models;


namespace NewLife.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<Car> Car { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Rent> Rent { get; set; }

    }
}
