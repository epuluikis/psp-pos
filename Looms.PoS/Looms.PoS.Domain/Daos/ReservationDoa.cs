using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record ReservationDao
{
    public Guid Id { get; init; }
    public Guid ServiceId { get; init; }
    public Guid EmployeeId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTime AppointmentTime { get; init; }
    public ReservationStatus Status { get; init; }
    public string Comment { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false;
}
