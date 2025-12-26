using CredirService.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CreditService.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly TimeProvider _dateTime;


        public AuditableEntityInterceptor(TimeProvider timeProvider)
        {
            _dateTime = timeProvider;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
                                                        InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                       InterceptionResult<int> result,
                                                       CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            try
            {
                if (context == null) return;

                foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
                {
                    if (entry.State is EntityState.Added or EntityState.Modified)
                    {
                        var utcNow = _dateTime.GetUtcNow();

                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.CreatedAt = utcNow;
                        }


                        entry.Entity.LastModifiedAt = utcNow;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }



    }

}
