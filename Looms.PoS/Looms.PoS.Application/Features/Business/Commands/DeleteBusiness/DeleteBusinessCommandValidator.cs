using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Business.Commands.DeleteBusiness;

public class DeleteBusinessCommandValidator : AbstractValidator<DeleteBusinessCommand>
{
    public DeleteBusinessCommandValidator(
        IBusinessesRepository businessesRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await businessesRepository.GetAsync(Guid.Parse(id)));
    }
}
