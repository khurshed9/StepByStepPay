namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;
    
    public decimal Price { get; set; }

    public string Condition { get; set; } = null!;

    public string Category { get; set; } = null!;
}
