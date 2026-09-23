using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(p => p.StockQuantity)
                .IsRequired();

            builder.HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId);

            builder.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            builder.HasData(
            // Electronics
            new Product
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                Name = "Wireless Headphones",
                Description = "Over-ear wireless headphones with noise cancellation",
                Price = 2499.99m,
                StockQuantity = 50,
                CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            },
            new Product
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                Name = "Smartphone",
                Description = "Modern smartphone with high-resolution display",
                Price = 15999.99m,
                StockQuantity = 30,
                CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            },
            new Product
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                Name = "Smart Watch",
                Description = "Smart watch with fitness and health tracking",
                Price = 3999.99m,
                StockQuantity = 40,
                CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            },

            // Computers
            new Product
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                Name = "Gaming Laptop",
                Description = "High-performance laptop for gaming and development",
                Price = 42999.99m,
                StockQuantity = 15,
                CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222")
            },
            new Product
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                Name = "Mechanical Keyboard",
                Description = "RGB mechanical keyboard with blue switches",
                Price = 1899.99m,
                StockQuantity = 60,
                CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222")
            },
            new Product
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse for everyday use",
                Price = 799.99m,
                StockQuantity = 100,
                CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222")
            },

            // Clothing
            new Product
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                Name = "Cotton T-Shirt",
                Description = "Comfortable cotton t-shirt",
                Price = 499.99m,
                StockQuantity = 100,
                CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            },
            new Product
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                Name = "Denim Jacket",
                Description = "Classic denim jacket",
                Price = 1499.99m,
                StockQuantity = 45,
                CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            },
            new Product
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                Name = "Running Shoes",
                Description = "Lightweight shoes for running and training",
                Price = 2299.99m,
                StockQuantity = 35,
                CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            },

            // Home & Kitchen
            new Product
            {
                Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                Name = "Coffee Maker",
                Description = "Automatic coffee maker for home use",
                Price = 2999.99m,
                StockQuantity = 25,
                CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444")
            },
            new Product
            {
                Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                Name = "Air Fryer",
                Description = "Digital air fryer with multiple cooking modes",
                Price = 3499.99m,
                StockQuantity = 20,
                CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444")
            },
            new Product
            {
                Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                Name = "Electric Kettle",
                Description = "Stainless steel electric kettle",
                Price = 899.99m,
                StockQuantity = 70,
                CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444")
            },

            // Sports
            new Product
            {
                Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                Name = "Yoga Mat",
                Description = "Non-slip exercise and yoga mat",
                Price = 599.99m,
                StockQuantity = 80,
                CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555")
            },
            new Product
            {
                Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                Name = "Dumbbell Set",
                Description = "Adjustable dumbbell set for home workouts",
                Price = 2499.99m,
                StockQuantity = 25,
                CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555")
            },
            new Product
            {
                Id = Guid.Parse("50000000-0000-0000-0000-000000000003"),
                Name = "Football",
                Description = "Professional size football",
                Price = 799.99m,
                StockQuantity = 50,
                CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555")
            }
        );
        }
    }
}
