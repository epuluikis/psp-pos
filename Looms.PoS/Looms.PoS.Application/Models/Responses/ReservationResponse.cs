using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses;

public class ReservationResponse
{
    public Guid Id { get; init; }
    public Guid ServiceId { get; init; }
    public Guid EmployeeId { get; init; }
    public Guid CustomerId { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime AppointmentTime { get; init; }
    public ReservationStatus Status { get; init; }
    public string Comment { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false;
}
