using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using Shared.Domain.Exception;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;

[PiplineOrder(3)]
public class IdempotencyBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> 
{
    
    private readonly ILogger<IdempotencyBehavior<TRequest,TResponse>> logger;
    private readonly IMemoryCache _cache;

    public IdempotencyBehavior(IMemoryCache cache,ILogger<IdempotencyBehavior<TRequest,TResponse>> _logger)
    {
        logger = _logger;
        _cache = cache;
        
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        if (request.RequestId==null||!request.RequestId.HasValue)
         {
             logger.LogWarning("Idempotency check failed: RequestId is null for {CommandType}", typeof(TRequest).Name);
             throw new IdempotencyException("Idempotency check failed: RequestId is null");
         }
         string cacheKey = $"Idempotent_{typeof(TRequest).Name}_{request.RequestId}";
         if (_cache.TryGetValue(cacheKey, out var cachedResult))
         {
             logger.LogInformation("Returning cached idempotent result for {CacheKey}", cacheKey);
             throw new IdempotencyException("Returning cached idempotent result for {CacheKey}");
              
         }
             
         var result = await next();
         _cache.Set(cacheKey, cacheKey,TimeSpan.FromSeconds(1));
         return result;

    }
}