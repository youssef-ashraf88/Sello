using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Data.Configuration
{
    public class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sa => sa.AddressLine)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sa => sa.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sa => sa.State)
                .HasMaxLength(100);

            builder.Property(sa => sa.PostalCode)
                .HasMaxLength(20);

            builder.Property(sa => sa.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sa => sa.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(sa => sa.User)
                .WithMany(u => u.ShippingAddresses)
                .HasForeignKey(sa => sa.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
