using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminal;

public class GetPaymentTerminalQueryHandler : IRequestHandler<GetPaymentTerminalQuery, IActionResult>
{
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;
    private readonly IPaymentTerminalModelsResolver _modelsResolver;

    public GetPaymentTerminalQueryHandler(
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IPaymentTerminalModelsResolver modelsResolver)
    {
        _paymentTerminalsRepository = paymentTerminalsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentTerminalQuery request, CancellationToken cancellationToken)
    {
        var paymentTerminalDao = await _paymentTerminalsRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(request.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(request.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(paymentTerminalDao);

        return new OkObjectResult(response);
    }
}
