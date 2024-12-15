using AutoMapper;
using Looms.PoS.Application.Features.Order.Queries.GetOrders;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Filters.Order;

namespace Looms.PoS.Application.Mappings.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<GetOrdersQuery, GetAllOrdersFilter>();

        CreateMap<OrderDao, OrderResponse>(MemberList.Source);

        CreateMap<CreateOrderRequest, OrderDao>(MemberList.Source);

        CreateMap<UpdateOrderRequest, OrderDao>(MemberList.Source)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));
    }
}