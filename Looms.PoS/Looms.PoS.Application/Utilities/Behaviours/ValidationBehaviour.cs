using FluentValidation;
using FluentValidation.Results;
using Looms.PoS.Application.Abstracts;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Dtos;
using Looms.PoS.Domain.Exceptions;
using MediatR;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : LoomsHttpRequest, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IValidator<BusinessIdDto> _businessIdValidator;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IValidator<BusinessIdDto> businessIdValidator)
    {
        _validators = validators;
        _businessIdValidator = businessIdValidator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var businessId = await ValidateBusinessIdHeaderAsync(request, cancellationToken);

        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        context.RootContextData[HeaderConstants.BusinessIdHeader] = businessId;
        var errors = _validators.Select(async x => await x.ValidateAsync(context))
                                .SelectMany(x => x.Result.Errors)
                                .Where(x => x is not null)
                                .ToList();

        ThrowIfErrors(errors);

        return await next();
    }

    private async Task<string> ValidateBusinessIdHeaderAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (request is GlobalLoomsHttpRequest)
        {
            return string.Empty;
        }

        if (!request.Request.Headers.TryGetValue(HeaderConstants.BusinessIdHeader, out var businessId)
            || businessId.Count == 0)
        {
            throw new LoomsBadRequestException($"{HeaderConstants.BusinessIdHeader} header not provided");
        }

        ThrowIfErrors((await _businessIdValidator.ValidateAsync(new(businessId!), cancellationToken)).Errors);

        return businessId!;
    }

    private static void ThrowIfErrors(IEnumerable<ValidationFailure> errors)
    {
        if (errors.Any())
        {
            throw new LoomsBadRequestException(string.Join("; ", errors.Select(x => x.ErrorMessage)));
        }
    }
}
