using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(c => c.Slug).IsUnique();
            
            entity.Property(c => c.Slug).HasMaxLength(250).IsRequired();
        });
        // Users - Orders: one-to-many
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        // Orders - PaymentDetails: one-to-one
        modelBuilder.Entity<Order>()
            .HasOne(e => e.PaymentDetail)
            .WithOne(e => e.Order)
            .HasForeignKey<PaymentDetail>(e => e.OrderId)
            .IsRequired();

        // Orders - OrderDetails: one-to-many 
        modelBuilder.Entity<OrderDetail>()
            .HasOne(e => e.Order)
            .WithMany(e => e.OrderDetails)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Products - OrderDetails: one-to-many
        modelBuilder.Entity<OrderDetail>()
            .HasOne(e => e.Product)
            .WithMany(e => e.OrderDetails)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Giải trí", Slug = "giai_tri", Description = "testest" },
            new Category { Id = 2, Name = "Làm việc", Slug = "lam_viec", Description = "testest" }
        );
        
        modelBuilder.Entity<Product>().HasData(
            new Product {Id = 1, Name = "Photoshop", Price = 1000000, Slug = "photoshop", Description = "photoshop", ImagePath = "https://logos-world.net/wp-content/uploads/2020/11/Adobe-Photoshop-Logo-2015-2019.png", StockQuantity = 100, IsActive = true, CategoryId = 2 },
            new Product {Id = 2, Name = "Dota 2", Price = 100000, Slug = "dota2", Description = "Dota 2", ImagePath = "https://cdn-icons-png.flaticon.com/512/588/588308.png", StockQuantity = 100, IsActive = true, CategoryId = 1 }
        );

        modelBuilder.Entity<User>().HasData(
            new User {Id = 1, Username = "admin", Password = "AdminPassword", Email = "admin@example.com", Phone = "0123456789", IsAdmin = true, IsActive = true},
            new User {Id = 2, Username = "user", Password = "UserPassword", Email = "user@example.com", Phone = "0123456789", IsAdmin = false, IsActive = true}
        );
    }
}
