using AutoMapper;
using Looms.PoS.Application.Features.Order.Queries.GetOrders;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.Order;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Filters.Order;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderModelsResolver : IOrderModelsResolver
{
    private readonly IMapper _mapper;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IRefundModelsResolver _refundModelsResolver;
    private readonly IOrderService _orderService;
    private readonly IRefundService _refundService;
    private readonly IPaymentService _paymentService;

    public OrderModelsResolver(
        IMapper mapper,
        IPaymentModelsResolver paymentModelsResolver,
        IOrderItemModelsResolver orderItemModelsResolver,
        IRefundModelsResolver refundModelsResolver,
        IOrderService orderService,
        IRefundService refundService,
        IPaymentService paymentService)
    {
        _mapper = mapper;
        _paymentModelsResolver = paymentModelsResolver;
        _orderItemModelsResolver = orderItemModelsResolver;
        _refundModelsResolver = refundModelsResolver;
        _orderService = orderService;
        _refundService = refundService;
        _paymentService = paymentService;
    }

    public GetAllOrdersFilter GetFiltersFromQuery(GetOrdersQuery getOrdersQuery)
    {
        return _mapper.Map<GetAllOrdersFilter>(getOrdersQuery);
    }

    public OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest, Guid userId, Guid businessId)
    {
        return _mapper.Map<OrderDao>(createOrderRequest) with { UserId = userId, BusinessId = businessId };
    }

    public OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest)
    {
        return _mapper.Map<OrderDao>(createOrderRequest);
    }

    public OrderDao GetDaoFromDaoAndRequest(OrderDao orderDao, UpdateOrderRequest updateOrderRequest)
    {
        return _mapper.Map(updateOrderRequest, orderDao) with { Id = orderDao.Id, OrderItems = orderDao.OrderItems };
    }

    public OrderDao GetDaoFromDaoAndStatus(OrderDao orderDao, OrderStatus orderStatus)
    {
        return _mapper.Map<OrderDao>(orderDao) with { Status = orderStatus };
    }

    public OrderResponse GetResponseFromDao(OrderDao orderDao)
    {
        return _mapper.Map<OrderResponse>(orderDao) with
        {
            BusinessName = orderDao.Business.Name,
            Payments = _paymentModelsResolver.GetResponseFromDao(orderDao.Payments),
            Refunds = _refundModelsResolver.GetResponseFromDao(orderDao.Refunds),
            OrderItems = _orderItemModelsResolver.GetResponseFromDao(orderDao.OrderItems),
            TotalAmount = _orderService.CalculateTotal(orderDao) + _paymentService.CalculateTips(orderDao.Payments),
            AmountPaid = _paymentService.CalculateTotalWithTips(orderDao.Payments),
            AmountRefunded = _refundService.CalculateTotal(orderDao.Refunds),
            TaxAmount = _orderService.CalculateTax(orderDao),
            TipAmount = _paymentService.CalculateTips(orderDao.Payments)
        };
    }

    public IEnumerable<OrderResponse> GetResponseFromDao(IEnumerable<OrderDao> orderDaos)
    {
        return orderDaos.Select(orderDao => GetResponseFromDao(orderDao));
    }

    public OrderDao GetDeletedDao(OrderDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }
}
