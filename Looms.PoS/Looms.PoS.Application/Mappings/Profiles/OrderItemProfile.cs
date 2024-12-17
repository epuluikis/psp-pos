using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.OrderItem;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<CreateOrderItemRequest, OrderItemDao>(MemberList.Source);

        CreateMap<UpdateOrderItemRequest, OrderItemDao>(MemberList.Source)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));

        CreateMap<OrderItemDao, OrderItemResponse>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : null));
    }
}
