using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;

public class CreatePaymentTerminalCommandHandler : IRequestHandler<CreatePaymentTerminalCommand, IActionResult>
{
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentTerminalModelsResolver _modelsResolver;

    public CreatePaymentTerminalCommandHandler(
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IHttpContentResolver httpContentResolver,
        IPaymentTerminalModelsResolver modelsResolver)
    {
        _paymentTerminalsRepository = paymentTerminalsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreatePaymentTerminalCommand command, CancellationToken cancellationToken)
    {
        var paymentTerminalRequest = await _httpContentResolver.GetPayloadAsync<CreatePaymentTerminalRequest>(command.Request);

        var paymentTerminalDao = _modelsResolver.GetDaoFromRequest(paymentTerminalRequest);
        var createdPaymentTerminalDao = await _paymentTerminalsRepository.CreateAsync(paymentTerminalDao);

        var response = _modelsResolver.GetResponseFromDao(createdPaymentTerminalDao);

        return new CreatedAtRouteResult($"/paymentterminals/{paymentTerminalDao.Id}", response);
    }
}
