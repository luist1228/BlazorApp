using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(c => c.ManufactureMethod)
                .HasConversion<int>();

            modelBuilder.Entity<Inventory>()
                .Property(c => c.IO)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Franela",
                    ManufactureMethod = Methods.HandMachineMade,
                    Stock = 0,
                },
                new Product
                {
                    Id = 2,
                    Name = "Vestido",
                    ManufactureMethod = Methods.HandMade,
                    Stock = 0,
                },
                new Product
                {
                    Id=3,
                    Name = "Zapato",
                    ManufactureMethod = Methods.HandMachineMade,
                    Stock = 0,
                }
                );
        }
    }
}
