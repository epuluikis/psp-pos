using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Models.Responses.Reservation;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IReservationModelsResolver
{
    ReservationDao GetDaoFromRequest(CreateReservationRequest createReservationRequest);
    ReservationDao GetDaoFromDaoAndRequest(ReservationDao originalDao, UpdateReservationRequest updateReservationRequest);
    ReservationDao GetDeletedDao(ReservationDao originalDao);
    ReservationResponse GetResponseFromDao(ReservationDao reservationDao);
    IEnumerable<ReservationResponse> GetResponseFromDao(IEnumerable<ReservationDao> reservationDao);
}
