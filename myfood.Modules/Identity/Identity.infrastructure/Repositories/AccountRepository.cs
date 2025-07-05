using Identity.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Shared.DDD.Base.Repository;

namespace Identity.infrastructure.Repositories;

public class AccountRepository: BaseRepository<User>,IAccountRepository
{
    public AccountRepository(myFoodDbContext context) : base(context)
    {
    }
}