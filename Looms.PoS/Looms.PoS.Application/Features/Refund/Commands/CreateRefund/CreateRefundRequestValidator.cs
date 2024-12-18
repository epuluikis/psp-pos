using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Refund.Commands.CreateRefund;

public class CreateRefundRequestValidator : AbstractValidator<CreateRefundRequest>
{
    public CreateRefundRequestValidator(
        IOrdersRepository ordersRepository,
        IPaymentsRepository paymentsRepository,
        IRefundService refundService
    )
    {
        RuleFor(x => x.Amount)
            .PrecisionScale(10, 2, false)
            .GreaterThan(0);

        RuleFor(x => x.RefundReason)
            .NotEmpty();

        RuleFor(x => x.OrderId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustBeValidGuid()
            .CustomAsync(async (orderId, context, _) =>
                {
                    var orderDao = await ordersRepository.GetAsyncByIdAndBusinessId(
                        Guid.Parse(orderId!),
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (orderDao.Status is not (OrderStatus.Locked or OrderStatus.Paid or OrderStatus.Refunded))
                    {
                        context.AddFailure($"Cannot refund order with status {orderDao.Status}.");
                    }
                }
            );

        RuleFor(x => x.PaymentId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustBeValidGuid()
            .CustomAsync(async (paymentId, context, _) =>
                {
                    if (!Guid.TryParse(context.InstanceToValidate.OrderId, out var orderId))
                    {
                        return;
                    }

                    var paymentDao = await paymentsRepository.GetAsyncByIdAndOrderIdAndBusinessId(
                        Guid.Parse(paymentId!),
                        orderId,
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (paymentDao.Status is not PaymentStatus.Succeeded)
                    {
                        context.AddFailure($"Cannot refund payment with status {paymentDao.Status}.");

                        return;
                    }

                    if (context.InstanceToValidate.Amount > refundService.CalculateRefundableAmountForPayment(paymentDao))
                    {
                        context.AddFailure("Amount is higher than payment refundable amount.");
                    }
                }
            );
    }
}
