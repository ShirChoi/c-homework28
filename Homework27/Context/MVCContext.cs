using System;
using Homework28.Models;
using Microsoft.EntityFrameworkCore;


namespace Homework28.Context {
    public class MVCContext : DbContext {
        public MVCContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ID = 1, Name = "Vegetables" },
                new ProductCategory { ID = 2, Name = "Fruits"     },
                new ProductCategory { ID = 3, Name = "Berries"    }
            );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
