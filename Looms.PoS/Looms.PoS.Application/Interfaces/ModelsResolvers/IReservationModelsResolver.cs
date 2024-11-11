using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IReservationModelsResolver
{
    ReservationDao GetDaoFromRequest(CreateReservationRequest createReservationRequest);
    ReservationResponse GetResponseFromDao(ReservationDao reservationDao);
    IEnumerable<ReservationResponse> GetResponseFromDao(IEnumerable<ReservationDao> reservationDao);
}
