namespace StockManagementSystem.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Data.Models;

    public class SMSDbContext : IdentityDbContext
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.ArticlesInThisCategory)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Article>()
                .HasOne(a => a.Supplier)
                .WithMany(s => s.ArticlesFromThisSupplier)
                .HasForeignKey(a => a.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Article>()
                .Property(a => a.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(builder);
        }
    }
}
