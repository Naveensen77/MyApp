using Microsoft.EntityFrameworkCore;


namespace MyApp.Common.Core.Data
{
    public abstract class BaseDbContext(DbContextOptions options, string schema = "dbo")
    : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Auto-discovers all IEntityTypeConfiguration<T> in the CONCRETE type's assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
