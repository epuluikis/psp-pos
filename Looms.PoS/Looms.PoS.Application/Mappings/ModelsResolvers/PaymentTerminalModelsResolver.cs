using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Models.Responses.PaymentTerminal;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class PaymentTerminalModelsResolver : IPaymentTerminalModelsResolver
{
    private readonly IMapper _mapper;

    public PaymentTerminalModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PaymentTerminalDao GetDaoFromRequest(CreatePaymentTerminalRequest createPaymentTerminalRequest)
    {
        return _mapper.Map<PaymentTerminalDao>(createPaymentTerminalRequest);
    }

    public PaymentTerminalDao GetDaoFromDaoAndRequest(
        PaymentTerminalDao originalDao,
        UpdatePaymentTerminalRequest updatePaymentTerminalRequest)
    {
        return _mapper.Map<PaymentTerminalDao>(updatePaymentTerminalRequest) with { Id = originalDao.Id, IsDeleted = originalDao.IsDeleted };
    }

    public PaymentTerminalDao GetDeletedDao(PaymentTerminalDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }

    public PaymentTerminalResponse GetResponseFromDao(PaymentTerminalDao paymentTerminalDao)
    {
        return _mapper.Map<PaymentTerminalResponse>(paymentTerminalDao);
    }

    public IEnumerable<PaymentTerminalResponse> GetResponseFromDao(IEnumerable<PaymentTerminalDao> paymentTerminalDao)
    {
        return _mapper.Map<IEnumerable<PaymentTerminalResponse>>(paymentTerminalDao);
    }
}
