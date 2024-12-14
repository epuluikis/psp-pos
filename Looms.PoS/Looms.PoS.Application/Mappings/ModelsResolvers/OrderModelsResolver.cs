using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderModelsResolver : IOrderModelsResolver
{
    private readonly IMapper _mapper;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IRefundModelsResolver _refundModelsResolver;

// TODO: Add same thing as for business for user 

    public OrderModelsResolver(IMapper mapper,
        IPaymentModelsResolver paymentModelsResolver,
        IOrderItemModelsResolver orderItemModelsResolver,
        IRefundModelsResolver refundModelsResolver)
    {
        _mapper = mapper;
        _paymentModelsResolver = paymentModelsResolver;
        _orderItemModelsResolver = orderItemModelsResolver;
        _refundModelsResolver = refundModelsResolver;
    }

    public OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest, BusinessDao businessDao, UserDao userDao)
    {
        return _mapper.Map<OrderDao>(createOrderRequest) with 
        {
            Business = businessDao,
            User = userDao
        };
    }

    public OrderDao GetDaoFromDaoAndRequest(OrderDao orderDao, UpdateOrderRequest updateOrderRequest, DiscountDao? discountDao)
    {
        return _mapper.Map(updateOrderRequest, orderDao) with
        {
            Id = orderDao.Id,
            Discount = discountDao,
            OrderItems = orderDao.OrderItems
        };
    }

    public OrderResponse GetResponseFromDao(OrderDao orderDao)
    {
        return _mapper.Map<OrderResponse>(orderDao) with
        {
            BusinessName = orderDao.Business.Name,
            Payments = _paymentModelsResolver.GetResponseFromDao(orderDao.Payments),
            Refunds = _refundModelsResolver.GetResponseFromDao(orderDao.Refunds),
            OrderItems = _orderItemModelsResolver.GetResponseFromDao(orderDao.OrderItems),
            TotalAmount = TotalsHelper.CalculateOrderTotal(orderDao),
            AmountPaid = TotalsHelper.CalculatePaymentTotal(orderDao.Payments),
            AmountRefunded = TotalsHelper.CalculateRefundTotal(orderDao.Refunds),
            TaxAmount = TotalsHelper.CalculateOrderTax(orderDao)
        };
    }

    public IEnumerable<OrderResponse> GetResponseFromDao(IEnumerable<OrderDao> orderDaos)
    {
        return orderDaos.Select(orderDao => GetResponseFromDao(orderDao));
    }

    public OrderDao GetDeletedDao(OrderDao originalDao){
        return originalDao with
        {
            IsDeleted = true
        };
    }
}