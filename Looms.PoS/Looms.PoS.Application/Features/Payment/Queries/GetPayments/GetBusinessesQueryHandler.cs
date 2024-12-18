﻿using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Queries.GetPayments;

public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, IActionResult>
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentModelsResolver _modelsResolver;

    public GetPaymentsQueryHandler(IPaymentsRepository paymentsRepository, IPaymentModelsResolver modelsResolver)
    {
        _paymentsRepository = paymentsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentsQuery query, CancellationToken cancellationToken)
    {
        var paymentDaos = await _paymentsRepository.GetAllAsyncByBusinessId(
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(paymentDaos);

        return new OkObjectResult(response);
    }
}
