using cw15.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace cw15.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=ProductCW15;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                Name = "Mobile",
            },
            new Category()
            {
                Id = 2,
                Name = "Laptop"
            },
            new Category()
            {
                Id = 3,
                Name = "TV"
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Brand = "Samsung",
                Color = "white",
                Name = "A33",
                CategoryId = 1,
                Price = 850,
                Stock = 10,
            },
            new Product()
            {
                Id = 2,
                Brand = "Sony",
                Color = "Black",
                Name = "z4",
                CategoryId = 1,
                Price = 400,
                Stock = 100,
            },
            new Product()
            {
                Id = 3,
                Brand = "Samsung",
                Color = "Black",
                Name = "S25",
                CategoryId = 1,
                Price = 1200,
                Stock = 50,
            },
            new Product()
            {
                Id = 4,
                Brand = "Samsung",
                Color = "white",
                Name = "acn455",
                CategoryId = 3,
                Price = 850,
                Stock = 10,
            },
            new Product()
            {
                Id = 5,
                Brand = "Asus",
                Color = "Black",
                Name = "fx707zc",
                CategoryId = 2,
                Price = 1000,
                Stock = 40,
            },
            new Product()
            {
                Id = 6,
                Brand = "Xiaomi",
                Color = "white",
                Name = "Poco x6",
                CategoryId = 1,
                Price = 900,
                Stock = 130,
            },
            new Product()
            {
                Id = 7,
                Brand = "Xiaomi",
                Color = "white",
                Name = "Tv3",
                CategoryId = 3,
                Price = 1000,
                Stock = 130,
            },
            new Product()
            {
                Id = 8,
                Brand = "Sony",
                Color = "Black",
                Name = "x555l",
                CategoryId = 3,
                Price = 900,
                Stock = 100,
            },
            new Product()
            {
                Id = 9,
                Brand = "Xiaomi",
                Color = "white",
                Name = "Poco x3",
                CategoryId = 1,
                Price = 600,
                Stock = 50,
            },
            new Product()
            {
                Id = 10,
                Brand = "Asus",
                Color = "Blue",
                Name = "f15",
                CategoryId = 2,
                Price = 900,
                Stock = 80,
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
