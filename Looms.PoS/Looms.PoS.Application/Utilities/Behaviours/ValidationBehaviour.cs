using FluentValidation;
using MediatR;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var errors = _validators.Select(async x => await x.ValidateAsync(context))
                                .SelectMany(x => x.Result.Errors)
                                .Where(x => x is not null)
                                .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
