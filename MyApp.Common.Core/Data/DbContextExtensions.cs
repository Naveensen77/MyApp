using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyApp.Common.Core.Data
{
    public static class DbContextExtensions
    {
        // Load a single navigation reference
        public static async Task LoadReferenceAsync<TEntity, TProperty>(
            this DbContext dbContext,
            TEntity entity,
            Expression<Func<TEntity, TProperty?>> navigationProperty,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TProperty : class
        {
            await dbContext.Entry(entity)
                .Reference(navigationProperty)
                .LoadAsync(cancellationToken);
        }
        // Load two navigation references at once
        public static async Task LoadReferencesAsync<TEntity>(
            this DbContext dbContext,
            TEntity entity,
            Expression<Func<TEntity, object?>> nav1,
            Expression<Func<TEntity, object?>> nav2,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            EntityEntry<TEntity> entry = dbContext.Entry(entity);
            await entry.Reference(nav1).LoadAsync(cancellationToken);
            await entry.Reference(nav2).LoadAsync(cancellationToken);
        }
    }
}
