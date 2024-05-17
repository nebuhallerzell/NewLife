using Microsoft.EntityFrameworkCore;
using NewLife.Models;


namespace NewLife.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<CarBrands> Car_Brand { get; set; }
        public DbSet<Car> Car { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
