﻿using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record RefundDao
{
    public Guid Id { get; init; }

    public Guid OrderId { get; init; }

    public Guid PaymentId { get; init; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; init; }

    public string RefundReason { get; init; } = string.Empty;

    public RefundStatus Status { get; init; } = RefundStatus.Pending;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; init; }

    public Guid UserId { get; init; }

    public string? ExternalId { get; init; }


    public virtual OrderDao Order { get; init; } = null!;
    public virtual PaymentDao Payment { get; init; } = null!;
    public virtual UserDao User { get; init; } = null!;
}
