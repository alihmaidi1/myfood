using Identity.Domain.Repository;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    public IAdminRepository _adminRepository { get; }
    public IJwtRepository _jwtRepository { get; }


    public myFoodIdentityDbContext _context { get; }
    public UnitOfWork(myFoodIdentityDbContext context,
        IJwtRepository jwtRepository,
        IAdminRepository adminRepository)
    {
        _context = context;
        _jwtRepository = jwtRepository;
        _adminRepository = adminRepository;
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async  Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        =>  await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }





    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }


    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();
    }
}