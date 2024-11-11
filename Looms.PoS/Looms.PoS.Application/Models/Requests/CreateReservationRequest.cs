using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests;

public record CreateReservationRequest
{
    public Guid ServiceId { get; init; }
    public Guid EmployeeId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTime AppointmentTime { get; init; }
    public ReservationStatus Status { get; init; }
    public string? Comment { get; init; }

}
