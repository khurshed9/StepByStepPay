namespace Application.Filter;

public record ProductFilter(
    string? Name,
    string? Condition,
    decimal? Price) : BaseFilter;