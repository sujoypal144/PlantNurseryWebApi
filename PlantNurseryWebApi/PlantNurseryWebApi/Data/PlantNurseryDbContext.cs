using Microsoft.EntityFrameworkCore;
using PlantNurseryWebApi;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PlantNurseryWebApi.Data
{
    public class PlantNurseryDbContext : DbContext
    {
        public PlantNurseryDbContext(DbContextOptions<PlantNurseryDbContext> options)
        : base(options)
        {
        }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Purchases> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

