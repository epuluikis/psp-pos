using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record UserDao
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;

    [Index(IsUnique = true)]
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = null!;

    [Column(TypeName = "smallint")]
    public UserRole Role { get; init; }

    public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; }

    public virtual BusinessDao Business { get; init; } = null!;
    public virtual ICollection<ReservationDao> Reservations { get; init; } = [];
}
