using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

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

    public ReservationResponse GetResponseFromDao(ReservationDao reservationDao)
        => _mapper.Map<ReservationResponse>(reservationDao);

    public IEnumerable<ReservationResponse> GetResponseFromDao(IEnumerable<ReservationDao> reservationDao)
        => _mapper.Map<IEnumerable<ReservationResponse>>(reservationDao);
}
