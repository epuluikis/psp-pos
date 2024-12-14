using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;


namespace Looms.PoS.Application.Mappings.Profiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDao, OrderResponse>(MemberList.Source);

        CreateMap<CreateOrderRequest, OrderDao>(MemberList.Source);

        CreateMap<UpdateOrderRequest, OrderDao>(MemberList.Source)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}