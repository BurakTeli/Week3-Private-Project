using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Entities;

namespace OnlineShopping.Data
{
    public class OnlineShoppingDbContext : DbContext
    {
        // Constructor: DbContext options are received via Dependency Injection.
        public OnlineShoppingDbContext(DbContextOptions<OnlineShoppingDbContext> options) : base(options) { }

        // Define a DbSet for each entity in the system.
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            
            modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });

            
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
