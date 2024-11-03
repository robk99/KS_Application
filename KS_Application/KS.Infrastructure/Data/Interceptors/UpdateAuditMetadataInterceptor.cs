using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using KS.Domain.Abstractions;

namespace KS.Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Sets createdAt on INSERT and updatedAt on INSERT/UPDATE 
    /// </summary>
    public class UpdateAuditDetailsInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditableEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateAuditableEntities(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries<AuditDetails>().ToList();

            foreach (EntityEntry<AuditDetails> entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    SetCurrentPropertyValue(
                        entry, nameof(AuditDetails.CreatedOnUtc), utcNow);
                    SetCurrentPropertyValue(
                        entry, nameof(AuditDetails.ModifiedOnUtc), utcNow);
                }

                if (entry.State == EntityState.Modified)
                {
                    SetCurrentPropertyValue(
                        entry, nameof(AuditDetails.ModifiedOnUtc), utcNow);
                }
            }

            static void SetCurrentPropertyValue(
                EntityEntry entry,
                string propertyName,
                DateTime utcNow) =>
                entry.Property(propertyName).CurrentValue = utcNow;
        }
    }
}
