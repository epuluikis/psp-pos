using AutoMapper;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Models.Responses.GiftCard;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class GiftCardProfile : Profile
{
    public GiftCardProfile()
    {
        CreateMap<CreateGiftCardRequest, GiftCardDao>(MemberList.Source)
            .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.ExpiryDate)));

        CreateMap<UpdateGiftCardRequest, GiftCardDao>(MemberList.Source)
            .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.ExpiryDate)));

        CreateMap<GiftCardDao, GiftCardResponse>();
    }
}
