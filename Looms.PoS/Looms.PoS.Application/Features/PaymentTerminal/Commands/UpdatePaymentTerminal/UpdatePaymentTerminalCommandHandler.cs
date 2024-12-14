using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;

public class UpdatePaymentTerminalCommandHandler : IRequestHandler<UpdatePaymentTerminalCommand, IActionResult>
{
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentTerminalModelsResolver _modelsResolver;

    public UpdatePaymentTerminalCommandHandler(
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IHttpContentResolver httpContentResolver,
        IPaymentTerminalModelsResolver modelsResolver)
    {
        _paymentTerminalsRepository = paymentTerminalsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdatePaymentTerminalCommand command, CancellationToken cancellationToken)
    {
        var updatePaymentTerminalRequest = await _httpContentResolver.GetPayloadAsync<UpdatePaymentTerminalRequest>(command.Request);

        var originalDao = await _paymentTerminalsRepository.GetAsync(Guid.Parse(command.Id));

        var paymentTerminalDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updatePaymentTerminalRequest);
        var updatedPaymentTerminalDao = await _paymentTerminalsRepository.UpdateAsync(paymentTerminalDao);

        var response = _modelsResolver.GetResponseFromDao(updatedPaymentTerminalDao);

        return new OkObjectResult(response);
    }
}
