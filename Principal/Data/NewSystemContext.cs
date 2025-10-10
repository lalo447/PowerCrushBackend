using Microsoft.EntityFrameworkCore;
using NewSystem.Domain.PowerCrushPlayer;
using NewSystem.Domain.PowerCrushProduct;

namespace NewSystem.Data
{
    /// <summary>Configuring the database connection.</summary>
    public class NewSystemContext : DbContext
    {
        public NewSystemContext(DbContextOptions<NewSystemContext> options) : base(options) { }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewSystemContext).Assembly);
        }
    }
}
