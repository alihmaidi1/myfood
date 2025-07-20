using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Domain.Repository;

public interface IUnitOfWork:IDisposable
{
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
}