﻿using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests.Discount;

public record CreateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public string DiscountType { get; init; } = string.Empty;
    public decimal Value { get; init; } = 0;
    public DiscountTarget DiscountTarget { get; init; }
    public string? ProductId { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
}
