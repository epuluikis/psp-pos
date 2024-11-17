using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests;

public record CreateReservationRequest
{
    public string CustomerId { get; init; } = string.Empty;
    public DateTime AppointmentTime { get; init; }
    public Guid ServiceId { get; init; }
    public Guid? EmployeeId { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

}
