using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewSystem.Domain.PowerCrushPlayer;

namespace NewSystem.Data.Schema
{
    public class PlayersSchema : IEntityTypeConfiguration<Players>
    {
        public void Configure(EntityTypeBuilder<Players> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired().HasAnnotation("Sqlite:Autoincrement", true).ValueGeneratedOnAdd().HasColumnName("Id");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(120).HasColumnName("Name");
            builder.Property(e => e.Points).IsRequired().HasColumnName("Points");
        }
    }
}
