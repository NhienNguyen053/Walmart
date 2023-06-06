#nullable disable

using Walmart.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Walmart.Infrastructure.Data
{
    public class WalmartContext : IdentityDbContext<ApplicationUser>
    {
        public WalmartContext (DbContextOptions<WalmartContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
            .HasMany(c => c.Subcategories)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.Category_Id);
            builder.Entity<SubCategory>()
            .HasMany(s => s.Products)
            .WithOne(p => p.SubCategory)
            .HasForeignKey(p => p.SubCategory_Id);
            builder.Entity<Product>()
            .HasMany(p => p.ProductPhotos)
            .WithOne(pp => pp.Product)
            .HasForeignKey(pp => pp.Product_Id);
            builder.Entity<ApplicationUser>()
            .HasMany(u => u.ShoppingCarts)
            .WithOne(sc => sc.User)
            .HasForeignKey(sc => sc.UserId);
            builder.Entity<ApplicationUser>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.ApplicationUser)
            .HasForeignKey(o => o.Customer_Id);
            builder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.Order_Id);
            builder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany()
            .HasForeignKey(od => od.Product_Id);

            builder.Entity<ShoppingCart>()
            .HasKey(c => new { c.UserId, c.ProductId });
            builder.Entity<OrderDetail>()
            .Property(d => d.Product_Price)
            .HasColumnType("decimal(18,2)");
            builder.Entity<Order>()
            .Property(e => e.Total_Price)
            .HasColumnType("decimal(18,2)");
            builder.Entity<Product>()
            .Property(e => e.Product_Price)
            .HasColumnType("decimal(18,2)");
            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("ApplicationUsers");
            });
            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserTokens");
            });

            builder.Entity<IdentityRole>(b =>
            {
                b.ToTable("Roles");
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("UserRoles");
            });
        }
    }
}
