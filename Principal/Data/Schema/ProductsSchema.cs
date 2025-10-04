using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewSystem.Domain.PowerCrushProduct;

namespace NewSystem.Data.Schema
{
    public class ProductsSchema : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {

            builder.ToTable("Products");
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).IsRequired().HasMaxLength(10).HasColumnName("Code");
            builder.Property(e => e.CategoryId).IsRequired().HasColumnName("CategoryId");
            builder.Property(e => e.IsComposed).IsRequired().HasColumnName("IsComposed");
            builder.Property(e => e.ImageUrl).IsRequired().HasMaxLength(400).HasColumnName("ImageUrl");
        }
    }
}
