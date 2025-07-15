using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Shared.Application.Decorator;

public class IdempotencyDecorator<TRequest>(
    IRequestHandler<TRequest> innerHandler,
    IMemoryCache _cache,
    ILogger<IdempotencyDecorator<TRequest>> logger) : IRequestHandler<TRequest>
    where TRequest : ICommand
{



    public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken)
    {if (request.RequestId==null)
        {
            logger.LogWarning("Idempotency check failed: RequestId is null for {CommandType}", typeof(TRequest).Name);
            return Result.ValidationFailure<object>(Error.ValidationFailures("Idempotency check failed: RequestId is null")).ToActionResult();
        }
        string cacheKey = $"Idempotent_{typeof(TRequest).Name}_{request.RequestId}";
        if (_cache.TryGetValue(cacheKey, out var cachedResult))
        {
            logger.LogInformation("Returning cached idempotent result for {CacheKey}", cacheKey);
            return Result.Conflict<object>(Error.AlreadyProcessed).ToActionResult();
             
        }
            
        var result = await innerHandler.Handle(request, cancellationToken);
        _cache.Set(cacheKey, cacheKey,TimeSpan.FromSeconds(60));
        return result;

    }
}