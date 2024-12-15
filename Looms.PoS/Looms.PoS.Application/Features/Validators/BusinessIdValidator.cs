using FluentValidation;
using Looms.PoS.Application.Models.Dtos;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Validators;

public class BusinessIdValidator : AbstractValidator<BusinessIdDto>
{
    public BusinessIdValidator(IBusinessesRepository businessesRepository)
    {
        RuleFor(x => x.BusinessId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (x, _, cancellationToken) => await businessesRepository.GetAsync(Guid.Parse(x)));
    }
}
