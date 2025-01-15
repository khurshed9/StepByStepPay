namespace Application.DTO_s;

public interface IBaseProductInfo
{
    public string Name { get; init; } 

    public string Color { get; init; } 
    
    public decimal Price { get; init; }

    public string Condition { get; init; } 

    public string Category { get; init; } 
}

public  record class ProductReadInfo(
    string Name,
    string Color,
    decimal Price,
    string Condition,
    string Category,
    int Id) : IBaseProductInfo;

public  record class ProductCreateInfo(
    string Name,
    string Color,
    decimal Price,
    string Condition,
    string Category) : IBaseProductInfo;


public readonly record struct ProductUpdateInfo(
    string Name,
    string Color,
    decimal Price,
    string Condition,
    string Category) : IBaseProductInfo;
