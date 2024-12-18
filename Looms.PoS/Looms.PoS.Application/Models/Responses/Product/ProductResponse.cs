﻿namespace Looms.PoS.Application.Models.Responses.Product;

public record ProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int QuantityInStock { get; init; } = 0;
    public Guid TaxId { get; init; }
    public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; }
}
