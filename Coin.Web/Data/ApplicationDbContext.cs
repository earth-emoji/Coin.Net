using Coin.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coin.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,IdentityRoleClaim<string>, IdentityUserToken<string>>
    {   
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
            
        }
        
        public string CurrentUserId { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Vendor)
            .WithOne(v => v.Identity)
            .HasForeignKey<Vendor>(m => m.IdentityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Customer)
            .WithOne(c => c.Identity)
            .HasForeignKey<Customer>(c => c.IdentityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Administrator)
            .WithOne(a => a.Identity)
            .HasForeignKey<Administrator>(a => a.IdentityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Vendor>()
            .HasOne(v => v.Identity)
            .WithOne(u => u.Vendor)
            .HasForeignKey<Vendor>(m => m.IdentityId);

        builder.Entity<Customer>()
            .HasOne(c => c.Identity)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(m => m.IdentityId);

        builder.Entity<Administrator>()
            .HasOne(a => a.Identity)
            .WithOne(u => u.Administrator)
            .HasForeignKey<Administrator>(a => a.IdentityId);
        }
    }
}