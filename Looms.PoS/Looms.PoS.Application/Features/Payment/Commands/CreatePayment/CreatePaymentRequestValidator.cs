using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator(
        IGiftCardsRepository giftCardsRepository,
        IPaymentTerminalsRepository paymentTerminalsRepository,
        IOrdersRepository ordersRepository,
        IOrderService orderService
    )
    {
        RuleFor(x => x.OrderId)
            .MustBeValidGuid()
            .CustomAsync(async (orderId, context, _) =>
                {
                    var orderDao = await ordersRepository.GetAsyncByIdAndBusinessId(
                        Guid.Parse(orderId!),
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (orderDao.Status is not (OrderStatus.Pending or OrderStatus.Locked))
                    {
                        context.AddFailure($"Cannot pay for order with status {orderDao.Status}.");
                    }
                }
            );

        RuleFor(x => x.Amount)
            .Cascade(CascadeMode.Stop)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0)
            .CustomAsync(async (amount, context, _) =>
                {
                    if (!Guid.TryParse(context.InstanceToValidate.OrderId, out var orderId))
                    {
                        return;
                    }

                    var orderDao = await ordersRepository.GetAsyncByIdAndBusinessId(
                        orderId,
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (amount > orderService.CalculatePayableAmount(orderDao))
                    {
                        context.AddFailure($"Amount is higher than order payable amount.");
                    }
                }
            );

        RuleFor(x => x.PaymentMethod)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.Tip)
            .Cascade(CascadeMode.Stop)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        When(x => x.PaymentMethod == PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .CustomAsync(async (code, context, _)
                    => await giftCardsRepository.GetAsyncByBusinessIdAndCode(
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader]),
                        code!)
                );
        });

        When(x => x.PaymentMethod == PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.PaymentTerminalId!)
                .Cascade(CascadeMode.Stop)
                .MustBeValidGuid()
                .CustomAsync(async (paymentTerminalId, context, _) =>
                {
                    var paymentTerminalDao = await paymentTerminalsRepository.GetAsyncByIdAndBusinessId(
                        Guid.Parse(paymentTerminalId!),
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (!paymentTerminalDao.IsActive)
                    {
                        context.AddFailure($"Payment terminal is not active.");
                    }

                    if (!paymentTerminalDao.PaymentProvider.IsActive)
                    {
                        context.AddFailure($"Payment provider is not active.");
                    }
                });
        });

        When(x => x.PaymentMethod != PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardCode)
                .Null();
        });

        When(x => x.PaymentMethod != PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.PaymentTerminalId)
                .Null();
        });
    }
}
