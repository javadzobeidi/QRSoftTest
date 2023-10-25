using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    void ChangeState(object entity, EntityState state);

    Task<IDbContextTransaction> BeginTransaction();
    DbSet<User> Users { get; }
    DbSet<UserToken> UserTokens { get; }

    DbSet<RegisterReferralLink> RegisterReferralLinks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
