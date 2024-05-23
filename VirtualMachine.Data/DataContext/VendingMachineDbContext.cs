using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualMachine.Data.Models;

namespace VirtualMachine.Data.DataContext
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext()
          : base()
        {
        }

        public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Build configuration
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=VirtualMachine; Encrypt=False; Integrated Security=True")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Dummy data
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Virtual Pet Cat", Price = 9.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 2, Name = "Virtual Pet Dog", Price = 9.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 3, Name = "Virtual Garden Kit", Price = 2.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 4, Name = "Virtual Music Album", Price = 6.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 5, Name = "Virtual Coffee", Price = 1.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 6, Name = "Virtual Energy Drink", Price = 3.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 7, Name = "Virtual Book", Price = 5.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 8, Name = "Virtual Plant", Price = 2.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 9, Name = "Virtual Adventure Pass", Price = 3.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 10, Name = "Virtual Art Piece", Price = 7.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 11, Name = "Virtual Game Token", Price = 1.90M, Quantity = new Random().Next(0, 99) },
                new Product { Id = 12, Name = "Virtual Sunglasses", Price = 1.90M, Quantity = new Random().Next(0, 99) }
            );
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
