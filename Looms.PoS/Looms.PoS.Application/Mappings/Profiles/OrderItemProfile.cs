using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

// TODO: Add for response mapping for product, variation and service names 

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<CreateOrderItemRequest, OrderItemDao>(MemberList.Source);

        CreateMap<UpdateOrderItemRequest, OrderItemDao>(MemberList.Source)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));

        CreateMap<OrderItemDao, OrderItemResponse>();


    }
}
