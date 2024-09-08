using Microsoft.EntityFrameworkCore;
using BundleTree.Models;

namespace BundleTree.Data
{
    public class BundleContext : DbContext
    {
        public BundleContext(DbContextOptions<BundleContext> options) : base(options) { }

        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BundlePart> BundleParts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Bundle to BundlePart relationship
            modelBuilder.Entity<Bundle>()
                .HasMany(b => b.BundleParts)
                .WithOne(bp => bp.Bundle)
                .HasForeignKey(bp => bp.BundleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure BundlePart to Bundle (SubBundle) relationship
            modelBuilder.Entity<BundlePart>()
                .HasOne(bp => bp.SubBundle)
                .WithMany()
                .HasForeignKey(bp => bp.SubBundleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure BundlePart to Product relationship
            modelBuilder.Entity<BundlePart>()
                .HasOne(bp => bp.Product)
                .WithMany()
                .HasForeignKey(bp => bp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
