using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Models.Responses.Reservation;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class ReservationModelsResolver : IReservationModelsResolver
{
    private readonly IMapper _mapper;

    public ReservationModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ReservationDao GetDaoFromRequest(CreateReservationRequest createReservationRequest)
        => _mapper.Map<ReservationDao>(createReservationRequest);

    public ReservationDao GetDaoFromDaoAndRequest(ReservationDao originalDao, UpdateReservationRequest updateReservationRequest)
    {
        return _mapper.Map<ReservationDao>(updateReservationRequest) with
        {
            Id = originalDao.Id
        };
    }

    public ReservationDao GetDeletedDao(ReservationDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }

    public ReservationDao GetUpdatedStatusDao (ReservationDao originalDao, ReservationStatus status)
    {
        return originalDao with
        {
            Status = status
        };
    }

    public ReservationResponse GetResponseFromDao(ReservationDao reservationDao)
        => _mapper.Map<ReservationResponse>(reservationDao);

    public IEnumerable<ReservationResponse> GetResponseFromDao(IEnumerable<ReservationDao> reservationDao)
        => _mapper.Map<IEnumerable<ReservationResponse>>(reservationDao);
}
