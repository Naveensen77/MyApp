using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MyApp.Common.Core.Data
{
    public sealed class AuditCommandInterceptor : SaveChangesInterceptor
    {
        private readonly IUserInfoAccessor _userInfoAccessor;
        public AuditCommandInterceptor(IUserInfoAccessor userInfoAccessor)
        {
            _userInfoAccessor = userInfoAccessor
                ?? throw new ArgumentNullException(nameof(userInfoAccessor));
        }
        // Sync path
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
                UpdateAuditEntity(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        // Async path
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
                UpdateAuditEntity(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private void UpdateAuditEntity(DbContext context)
        {
            if (!context.ChangeTracker.HasChanges()) return;
            IEnumerable<EntityEntry<AuditEntity>> entities =
                context.ChangeTracker.Entries<AuditEntity>();
            foreach (EntityEntry<AuditEntity> entry in entities)
            {
                // Only stamp Added or Modified — skip Deleted/Unchanged
                if (entry.State is not (EntityState.Added or EntityState.Modified))
                    continue;
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.IpAddress = _userInfoAccessor.GetRemoteIp();
                entry.Entity.CreatedBy = _userInfoAccessor.GetUserName();
            }
        }
    }
}
