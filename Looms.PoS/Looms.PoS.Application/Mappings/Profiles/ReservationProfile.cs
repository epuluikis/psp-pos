using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationRequest, ReservationDao>(MemberList.Source);

        CreateMap<ReservationDao, ReservationResponse>();
    }
}
