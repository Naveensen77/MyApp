using Microsoft.EntityFrameworkCore;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using MyApp.Common.Core.Data;

namespace MyApp.CareerAdvancement.Core.Data
{
    public abstract class CareerAdvancementSchemeDbContext(DbContextOptions options)
    : BaseDbContext(options, SchemaNames.CAS)
    {
        public DbSet<AssessmentSession> AssessmentSessions => Set<AssessmentSession>();
    }
}
