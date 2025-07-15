
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Domain.CQRS;

namespace Shared.Application.Decorator;

public class LoggingDecorator<TRequest>(IRequestHandler<TRequest> innerHandler,ILogger<LoggingDecorator<TRequest>> logger) : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    
    
    public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - RequestData={RequestData}",
            typeof(TRequest).Name, request);
        var timer = new Stopwatch();
        timer.Start();
        var response=await innerHandler.Handle(request, cancellationToken);
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);

        logger.LogInformation("[END] Handled {Request}", typeof(TRequest).Name);
        return response;

    }

}