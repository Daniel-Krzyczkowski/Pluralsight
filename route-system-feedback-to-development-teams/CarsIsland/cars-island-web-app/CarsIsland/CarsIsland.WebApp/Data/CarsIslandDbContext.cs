using CarsIsland.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsIsland.WebApp.Data
{
    public class CarsIslandDbContext : DbContext
    {
        public CarsIslandDbContext(DbContextOptions<CarsIslandDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
