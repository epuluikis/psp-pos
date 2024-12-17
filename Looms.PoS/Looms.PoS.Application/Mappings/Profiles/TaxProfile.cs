using AutoMapper;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Application.Models.Responses.Tax;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.Profiles;

public class TaxProfile : Profile
{
    public TaxProfile()
    {
        CreateMap<CreateTaxRequest, TaxDao>(MemberList.Source)
            .ForMember(dest => dest.TaxCategory, opt => opt.MapFrom(src => Enum.Parse<TaxCategory>(src.TaxCategory, true)))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => 
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.EndDate));
                opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate));
            });

        CreateMap<UpdateTaxRequest, TaxDao>(MemberList.Source)
            .ForMember(dest => dest.TaxCategory, opt => opt.MapFrom(src => Enum.Parse<TaxCategory>(src.TaxCategory, true)))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => 
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.EndDate));
                opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate));
            });

        CreateMap<TaxDao, TaxResponse>();
    }
}