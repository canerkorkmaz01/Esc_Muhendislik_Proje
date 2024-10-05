using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_App_Data.Configuration
{
    public class ProductEntityTypeBuilderConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.ProductCode).IsRequired();
            builder.Property(p => p.ProductCode).HasMaxLength(30);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.ProductName).HasMaxLength(50);
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.UnitPrice).HasPrecision(18, 2);
        }
    }
}
