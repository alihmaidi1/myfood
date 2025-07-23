using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Domain.Repository;

public interface IUnitOfWork:IDisposable
{
    
    IAdminRepository _adminRepository { get; }
    
    IJwtRepository _jwtRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
}