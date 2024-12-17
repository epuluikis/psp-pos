using FluentValidation;
using Looms.PoS.Application.Models.Requests;
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
            .MustBeValidGuid()
            .CustomAsync(async (businessId, _, cancellationToken) =>
                await businessesRepository.GetAsync(Guid.Parse(businessId)));

        RuleFor(x => x.UserId)
            .MustBeValidGuid()
            .CustomAsync(async (userId, _, cancellationToken) =>
                await usersRepository.GetAsync(Guid.Parse(userId)));
    }
}
