using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Models.Responses.Reservation;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IReservationModelsResolver
{
    ReservationDao GetDaoFromRequest(CreateReservationRequest createReservationRequest);
    ReservationDao GetDaoFromDaoAndRequest(ReservationDao originalDao, UpdateReservationRequest updateReservationRequest);
    ReservationDao GetDeletedDao(ReservationDao originalDao);
    ReservationDao GetUpdatedStatusDao (ReservationDao originalDao, ReservationStatus status);
    ReservationResponse GetResponseFromDao(ReservationDao reservationDao);
    IEnumerable<ReservationResponse> GetResponseFromDao(IEnumerable<ReservationDao> reservationDao);
}
