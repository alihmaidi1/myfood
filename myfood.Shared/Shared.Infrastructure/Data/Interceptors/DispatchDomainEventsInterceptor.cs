// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Shared.Domain.CQRS;
// using Shared.Domain.Entities;
//
// namespace Shared.Infrastructure.Data.Interceptors;
//
// public class DispatchDomainEventsInterceptor(IDomainEventDispatcher dispatcher): SaveChangesInterceptor
// {
//     
//     
//     public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//     {
//         DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
//         return base.SavingChanges(eventData, result);
//     }
//
//     public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//     {
//         await DispatchDomainEvents(eventData.Context);
//         return await base.SavingChangesAsync(eventData, result, cancellationToken);
//     }
//     
//     
//     private async Task DispatchDomainEvents(DbContext? context)
//     {
//         if (context == null) return;
//
//         var aggregates = context.ChangeTracker
//             .Entries<IAggregate>()
//             .Where(a => a.Entity._domainEvents.Any())
//             .Select(a => a.Entity);
//
//         var domainEvents = aggregates
//             .SelectMany(a => a._domainEvents)
//             .ToList();
//
//         aggregates.ToList().ForEach(a => a.ClearDomainEvents());
//         await dispatcher.DispatchAsync(domainEvents);
//     }
//     
// }