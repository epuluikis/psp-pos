using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderModelsResolver : IOrderModelsResolver
{
    private readonly IMapper _mapper;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IRefundModelsResolver _refundModelsResolver;
    private readonly IOrderTotalsService _orderTotalsService;
    private readonly IRefundsTotalsService _refundsTotalsService;
    private readonly IPaymentTotalsService _paymentTotalsService;

    public OrderModelsResolver(IMapper mapper,
        IPaymentModelsResolver paymentModelsResolver,
        IOrderItemModelsResolver orderItemModelsResolver,
        IRefundModelsResolver refundModelsResolver,
        IOrderTotalsService orderTotalsService,
        IRefundsTotalsService refundsTotalsService,
        IPaymentTotalsService paymentTotalsService)
    {
        _mapper = mapper;
        _paymentModelsResolver = paymentModelsResolver;
        _orderItemModelsResolver = orderItemModelsResolver;
        _refundModelsResolver = refundModelsResolver;
        _orderTotalsService = orderTotalsService;
        _refundsTotalsService = refundsTotalsService;
        _paymentTotalsService = paymentTotalsService;
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
            TotalAmount = _orderTotalsService.CalculateOrderTotal(orderDao),
            AmountPaid = _paymentTotalsService.CalculatePaymentTotal(orderDao.Payments),
            AmountRefunded = _refundsTotalsService.CalculateRefundTotal(orderDao.Refunds),
            TaxAmount = _orderTotalsService.CalculateOrderTax(orderDao)
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