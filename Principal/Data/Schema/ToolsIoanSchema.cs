using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.Data.Schema
{
    public class ToolsIoanSchema : IEntityTypeConfiguration<ToolsIoans>
    {
        public void Configure(EntityTypeBuilder<ToolsIoans> builder)
        {
            builder.ToTable("ToolsIoan");
            builder.HasKey(e => e.ToolIoanId);
            builder.Property(e => e.ToolIoanId).IsRequired().HasColumnName("ToolIoanId");
            builder.Property(e => e.ClientName).IsRequired().HasColumnName("ClientName");
            builder.Property(e => e.NumberIoan).IsRequired().HasColumnName("NumberIoan");
            builder.Property(e => e.TotalItems).IsRequired().HasColumnName("TotalItems");
            builder.Property(e => e.IsActive).IsRequired().HasColumnName("IsActive");
            builder.Property(e => e.CreatedDate).IsRequired().HasColumnName("CreatedDate");
            builder.Property(e => e.ReturnDate).IsRequired(false).HasColumnName("ReturnDate");
            builder.Property(e => e.ToolsLoanDetails).IsRequired().HasColumnName("ToolsLoanDetails");
            builder.Property(e => e.ToolsIoanStatus).IsRequired().HasColumnName("ToolsIoanStatus");
            builder.Property(e => e.Comments).IsRequired(false).HasColumnName("Comments");
        }
    }
}
