using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CreateDiscountRequest, DiscountDao>(MemberList.Source);
        CreateMap<UpdateDiscountRequest, DiscountDao>(MemberList.Source);
        CreateMap<DiscountDao, DiscountResponse>();
    }
}
