using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.CQRS;

namespace Shared.Application.CQRS;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> _handlerTypeCache = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo> _handleMethodCache = new();

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        // معالجة الأحداث بشكل متوازي إذا كانت مستقلة
        var dispatchTasks = domainEvents.Select(e => DispatchEventAsync(e, cancellationToken));
        await Task.WhenAll(dispatchTasks);
    }

    private async Task DispatchEventAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateAsyncScope();
        var eventType = domainEvent.GetType();
        
        // الحصول على نوع الـ handler مع التخزين المؤقت
        var handlerType = _handlerTypeCache.GetOrAdd(
            eventType,
            t => typeof(IDomainEventHandler<>).MakeGenericType(t));

        var handlers = scope.ServiceProvider.GetServices(handlerType);

        // تنفيذ جميع الـ handlers بشكل متوازي إذا كانت مستقلة
        var handleTasks = handlers
            .Where(handler => handler != null)
            .Select(handler => HandleSingleEventAsync(handler, domainEvent, cancellationToken));

        await Task.WhenAll(handleTasks);
    }

    private async Task HandleSingleEventAsync(object handler, IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            var handleMethod = _handleMethodCache.GetOrAdd(
                handler.GetType(),
                t => t.GetMethod("Handle") ?? throw new InvalidOperationException("Handle method not found"));

            await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
        }
        catch (Exception ex)
        {
            // يمكنك إضافة logging هنا
            throw new InvalidOperationException($"Error handling event {domainEvent.GetType().Name}", ex);
        }
    }
}