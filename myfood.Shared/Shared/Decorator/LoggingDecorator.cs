
namespace Shared.Decorator;

public class LoggingDecorator
{
    
    
    public  class CommandHandler<TCommand>(IRequestHandler<TCommand> innerHandler,ILogger<CommandHandler<TCommand>> logger) : ICommandHandler<TCommand>
        where TCommand : ICommand
    {


        public async Task<IResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - RequestData={RequestData}",
                typeof(TCommand).Name, request);
            var timer = new Stopwatch();
            timer.Start();
            var response=await innerHandler.Handle(request, cancellationToken);
            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                    typeof(TCommand).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handled {Request}", typeof(TCommand).Name);
            return response;




        }
    }


    public sealed class QueryHandler<TQuery>(IQueryHandler<TQuery> innerHandler,ILogger<QueryHandler<TQuery>> logger) : IQueryHandler<TQuery>
        where TQuery : IQuery 
    {
        public async Task<IResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - RequestData={RequestData}",
                typeof(TQuery).Name, request);
            var timer = new Stopwatch();
            timer.Start();
            var response=await innerHandler.Handle(request, cancellationToken);
            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                    typeof(TQuery).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handled {Request}", typeof(TQuery).Name);
            return response;


            
        }
    }


    
}