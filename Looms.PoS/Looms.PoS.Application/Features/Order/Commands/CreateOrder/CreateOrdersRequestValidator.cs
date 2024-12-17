using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator(
        IBusinessesRepository businessesRepository,
        IUsersRepository usersRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.BusinessId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (businessId, _, cancellationToken) =>
                await businessesRepository.GetAsync(Guid.Parse(businessId!)));

        RuleFor(x => x.UserId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (userId, context, cancellationToken) =>
                await usersRepository.GetByBusinessAsync(Guid.Parse(userId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])));
    }
}
