// using FluentValidation;
// using FluentValidation.Results;
// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Shared.Application.Decorator;
// public class ValidationDecorator<TRequest, THandler>(
//     THandler innerHandler,
//     IEnumerable<IValidator<TRequest>> validators)
//     : IRequestHandler<TRequest>
//     where TRequest : IRequest
//     where THandler : IRequestHandler<TRequest>
// {
//
//
//     public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken)
//     {
//         var validationFailures = await ValidateAsync(request, validators);
//         if (validationFailures == null)
//         {
//             return await innerHandler.Handle(request, cancellationToken);
//         }
//         return Result.ValidationFailure<object>(Error.ValidationFailures(validationFailures)).ToActionResult();
//
//     }
//
//
//
//
//
//
//     private static async Task<string?> ValidateAsync<TCommand>(TCommand request, IEnumerable<IValidator<TCommand>> validators)
//     {
//
//         if (!validators.Any())
//         {
//
//             return null;
//         }
//         var context = new ValidationContext<TCommand>(request);
//
//         ValidationResult[] validationResult = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context)));
//         var validationFailures = validationResult
//         .Where(validationResult => !validationResult.IsValid)
//         .SelectMany(validationResult => validationResult.Errors)
//         .ToList();
//
//         if (!validationFailures.Any())
//         {
//
//             return null;
//         }
//         else
//         {
//
//             return validationFailures.First().ErrorMessage;
//         }
//
//     }
//
//
// }