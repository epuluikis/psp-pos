namespace Looms.PoS.Application.Models.Requests.Reservation;

public record CreateReservationRequest
{
    public string CustomerName { get; init; } = string.Empty;
    public string AppointmentTime { get; init; } = string.Empty;
    public string ServiceId { get; init; } = string.Empty;
    public string EmployeeId { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

}
