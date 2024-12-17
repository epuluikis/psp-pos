using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Commands.CreateRefund;

public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, IActionResult>
{
    private readonly IRefundsRepository _refundsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IRefundModelsResolver _modelsResolver;

    public CreateRefundCommandHandler(
        IRefundsRepository refundsRepository,
        IHttpContentResolver httpContentResolver,
        IRefundModelsResolver modelsResolver)
    {
        _refundsRepository = refundsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
    {
        var refundRequest = await _httpContentResolver.GetPayloadAsync<CreateRefundRequest>(request.Request);

        //TODO: Extract user id from request

        var refundDao = _modelsResolver.GetDaoFromRequest(refundRequest);
        var createdRefundDao = await _refundsRepository.CreateAsync(refundDao);

        var response = _modelsResolver.GetResponseFromDao(createdRefundDao);

        return new CreatedAtRouteResult($"/refunds/{refundDao.Id}", response);
    }
}
