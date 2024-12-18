using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services.RefundHandler;

public class CreditCardRefundHandlerService : IRefundHandlerService
{
    private readonly IRefundsRepository _refundsRepository;
    private readonly IPaymentProviderServiceFactory _paymentProviderServiceFactory;

    public PaymentMethod SupportedMethod => PaymentMethod.CreditCard;

    public CreditCardRefundHandlerService(
        IRefundsRepository refundsRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory
    )
    {
        _refundsRepository = refundsRepository;
        _paymentProviderServiceFactory = paymentProviderServiceFactory;
    }

    public async Task<RefundDao> HandleRefund(RefundDao refundDao)
    {
        refundDao = await _paymentProviderServiceFactory
                          .GetService(refundDao.Payment.PaymentTerminal!.PaymentProvider.Type)
                          .HandleRefund(refundDao, refundDao.Payment, refundDao.Payment.PaymentTerminal.PaymentProvider);

        return await _refundsRepository.CreateAsync(refundDao);
    }
}
