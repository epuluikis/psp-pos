using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentModelsResolver _modelsResolver;
    private readonly IPaymentHandlerFactory _paymentHandlerFactory;

    public CreatePaymentCommandHandler(
        IHttpContentResolver httpContentResolver,
        IPaymentModelsResolver modelsResolver,
        IPaymentHandlerFactory paymentHandlerFactory
    )
    {
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _paymentHandlerFactory = paymentHandlerFactory;
    }

    public async Task<IActionResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var paymentRequest = await _httpContentResolver.GetPayloadAsync<CreatePaymentRequest>(command.Request);
        var paymentDao = _modelsResolver.GetDaoFromRequest(paymentRequest);
        var response = await _paymentHandlerFactory.GetHandler(paymentDao.PaymentMethod).HandlePayment(paymentDao);

        return new CreatedAtRouteResult($"/payments/{paymentDao.Id}", response);
    }
}
