namespace Looms.PoS.Application.Models.Requests;

public record CreateReservationRequest
{
    public string CustomerId { get; init; } = string.Empty;
    public string AppointmentTime { get; init; } = string.Empty;
    public string ServiceId { get; init; } = string.Empty;
    public string? EmployeeId { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

}
