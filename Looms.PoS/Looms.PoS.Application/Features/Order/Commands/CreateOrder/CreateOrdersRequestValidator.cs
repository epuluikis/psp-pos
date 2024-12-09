using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator(IBusinessesRepository businessesRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.BusinessId)
            .MustBeValidGuid()
            .CustomAsync(async (businessId, _, cancellationToken) => 
        await businessesRepository.GetAsync(Guid.Parse(businessId)));

        // TODO: add same rule for userId
    }
}
