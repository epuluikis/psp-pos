using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminals;

public class GetPaymentTerminalsQueryHandler : IRequestHandler<GetPaymentTerminalsQuery, IActionResult>
{
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;
    private readonly IPaymentTerminalModelsResolver _modelsResolver;

    public GetPaymentTerminalsQueryHandler(
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IPaymentTerminalModelsResolver modelsResolver)
    {
        _paymentTerminalsRepository = paymentTerminalsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentTerminalsQuery request, CancellationToken cancellationToken)
    {
        var paymentTerminalDaos = await _paymentTerminalsRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(paymentTerminalDaos);

        return new OkObjectResult(response);
    }
}
