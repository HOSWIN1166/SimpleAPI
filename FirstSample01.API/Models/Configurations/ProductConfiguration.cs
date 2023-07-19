using FirstSample01.API.Models.DomainAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstSample01.API.Models.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Name).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Description).IsRequired().HasMaxLength(1000);
            builder.Property(product => product.Price).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Quantity).IsRequired().HasMaxLength(100);
            builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
