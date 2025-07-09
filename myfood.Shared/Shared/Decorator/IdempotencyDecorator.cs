using Microsoft.Extensions.Caching.Memory;

namespace Shared.Decorator;

public class IdempotencyDecorator
{


    public class CommandHandler<TCommand>(
        IRequestHandler<TCommand> innerHandler,
        IMemoryCache _cache,
        ILogger<CommandHandler<TCommand>> logger) : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        
        public async Task<IResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestId==null)
            {
                logger.LogWarning("Idempotency check failed: RequestId is null for {CommandType}", typeof(TCommand).Name);
                Result.ValidationFailureResult<object>(Error.ValidationFailures("Idempotency check failed: RequestId is null"));
            }
            string cacheKey = $"Idempotent_{typeof(TCommand).Name}_{request.RequestId}";
            if (_cache.TryGetValue(cacheKey, out var cachedResult))
            {
                logger.LogInformation("Returning cached idempotent result for {CacheKey}", cacheKey);
                return Result.ProcessedFailureResult<object>();
             
            }
            
            var result = await innerHandler.Handle(request, cancellationToken);
            _cache.Set(cacheKey, cacheKey,TimeSpan.FromSeconds(60));
            return result;
            
        }
    }
    

}