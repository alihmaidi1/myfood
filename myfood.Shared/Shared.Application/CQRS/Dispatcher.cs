using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.CQRS;

namespace Shared.Application.CQRS;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly ConcurrentDictionary<Type, Type> _commandHandlerTypes = new();
    private static readonly ConcurrentDictionary<Type, Type> _queryHandlerTypes = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo> _handlerMethods = new();

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        var commandType = command.GetType();

        var handlerType = _commandHandlerTypes.GetOrAdd(commandType, 
            t => typeof(ICommandHandler<,>).MakeGenericType(t, typeof(TResult)));

        return await InvokeHandler<TResult>(handlerType, command, cancellationToken);
    }

    public async Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        var queryType = query.GetType();

        var handlerType = _queryHandlerTypes.GetOrAdd(queryType, 
            t => typeof(IQueryHandler<,>).MakeGenericType(t, typeof(TResult)));

        return await InvokeHandler<TResult>(handlerType, query, cancellationToken);
    }

    private async Task<TResult> InvokeHandler<TResult>(Type handlerType, object request, CancellationToken cancellationToken)
    {
        try
        {
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var method = _handlerMethods.GetOrAdd(handlerType, 
                t => t.GetMethod("Handle") ?? throw new InvalidOperationException($"Handle method not found on {t.Name}"));
            var result = method.Invoke(handler, new[] { request, cancellationToken });

            return await (Task<TResult>)result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error invoking handler for {request.GetType().Name}", ex);
        }
    }
}