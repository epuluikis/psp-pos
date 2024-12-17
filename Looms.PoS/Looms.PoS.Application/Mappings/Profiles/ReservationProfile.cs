using AutoMapper;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Models.Responses.Reservation;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationRequest, ReservationDao>(MemberList.Source)
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => Guid.Parse(src.ServiceId)))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => Guid.Parse(src.EmployeeId)))
            .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.AppointmentTime)));

        CreateMap<UpdateReservationRequest, ReservationDao>(MemberList.Source)
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => Guid.Parse(src.ServiceId)))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => Guid.Parse(src.EmployeeId)))
            .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.AppointmentTime)));

        CreateMap<ReservationDao, ReservationResponse>()
            .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => DateTimeHelper.ConvertToLocal(src.AppointmentTime)));
    }
}
