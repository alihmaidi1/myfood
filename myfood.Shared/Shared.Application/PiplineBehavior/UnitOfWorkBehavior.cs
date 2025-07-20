using System.Transactions;
using Microsoft.AspNetCore.Http;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Application.PiplineBehavior;

[PiplineOrder(2)]
public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        // TransactionScopeOptions: Required, AsyncFlow, ReadCommitted
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            transactionOptions,
            TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = await next();
            scope.Complete();
            return result;
        }
    }
}