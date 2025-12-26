using CredirService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Credit> Credits { get; }
        Task<int> SaveChangedAsync(CancellationToken cancellationToken = default);
    }
}
