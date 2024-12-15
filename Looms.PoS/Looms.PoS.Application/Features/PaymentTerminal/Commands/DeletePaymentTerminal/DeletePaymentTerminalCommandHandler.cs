using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.DeletePaymentTerminal;

public class DeletePaymentTerminalCommandHandler : IRequestHandler<DeletePaymentTerminalCommand, IActionResult>
{
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;
    private readonly IPaymentTerminalModelsResolver _modelsResolver;

    public DeletePaymentTerminalCommandHandler(
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IPaymentTerminalModelsResolver modelsResolver)
    {
        _paymentTerminalsRepository = paymentTerminalsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeletePaymentTerminalCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _paymentTerminalsRepository.GetAsync(Guid.Parse(command.Id));

        var paymentTerminalDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _paymentTerminalsRepository.UpdateAsync(paymentTerminalDao);

        return new NoContentResult();
    }
}
