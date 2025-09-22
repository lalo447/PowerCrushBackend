using Microsoft.EntityFrameworkCore;
using NewSystem.Domain.PowerCrushPlayer;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.Data
{
    /// <summary>Configuring the database connection.</summary>
    public class NewSystemContext : DbContext
    {
        public NewSystemContext(DbContextOptions<NewSystemContext> options) : base(options) { }
        public virtual DbSet<ToolsIoans> ToolsIoan { get; set; }
        public virtual DbSet<Players> PowerCrushPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewSystemContext).Assembly);
        }
    }
}
