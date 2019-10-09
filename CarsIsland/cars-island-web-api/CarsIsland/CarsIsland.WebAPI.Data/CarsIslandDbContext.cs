using CarsIsland.WebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Data
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
