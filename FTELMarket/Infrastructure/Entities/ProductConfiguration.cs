using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTELMarket.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Price).HasPrecision(10, 2);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Product { Id = 1, Name = "The Last of Us Part II", Description = "Boîte et notice, disque sans rayure.", Price = 19.9m, Stock = 3, CategoryId = 1, CreatedAt = new DateTime(2026, 9, 2, 9, 15, 0, DateTimeKind.Utc) },
                new Product { Id = 2, Name = "Gran Turismo 7", Description = null, Price = 29.9m, Stock = 1, CategoryId = 1, CreatedAt = new DateTime(2026, 9, 5, 14, 30, 0, DateTimeKind.Utc) },
                new Product { Id = 3, Name = "The Legend of Zelda: Tears of the Kingdom", Description = "Cartouche seule.", Price = 39.9m, Stock = 2, CategoryId = 2, CreatedAt = new DateTime(2026, 9, 8, 10, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 4, Name = "Mario Kart 8 Deluxe", Description = "Complet, très bon état.", Price = 34.9m, Stock = 5, CategoryId = 2, CreatedAt = new DateTime(2026, 9, 10, 16, 45, 0, DateTimeKind.Utc) },
                new Product { Id = 5, Name = "Halo Infinite", Description = null, Price = 14.9m, Stock = 4, CategoryId = 3, CreatedAt = new DateTime(2026, 9, 12, 11, 20, 0, DateTimeKind.Utc) },
                new Product { Id = 6, Name = "Forza Horizon 5", Description = "Édition standard.", Price = 24.9m, Stock = 0, CategoryId = 3, CreatedAt = new DateTime(2026, 9, 15, 8, 5, 0, DateTimeKind.Utc) },
                new Product { Id = 7, Name = "Baldur’s Gate 3", Description = "Clé physique, coffret collector.", Price = 44.9m, Stock = 1, CategoryId = 4, CreatedAt = new DateTime(2026, 9, 18, 17, 40, 0, DateTimeKind.Utc) },
                new Product { Id = 8, Name = "Hades", Description = null, Price = 9.9m, Stock = 6, CategoryId = 4, CreatedAt = new DateTime(2026, 9, 21, 13, 10, 0, DateTimeKind.Utc) });
        }
    }
}