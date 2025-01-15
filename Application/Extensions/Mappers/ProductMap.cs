namespace Application.Extensions.Mappers;

public static class ProductMap
{
    public static ProductReadInfo ToRead(this Product product)
    {
        return new ProductReadInfo(
            product.Name,
            product.Color,
            product.Price,
            product.Condition,
            product.Category,
            product.Id);
    }

    public static Product ToEntity(this ProductCreateInfo createInfo)
    {

        return new Product()
        {
            Name = createInfo.Name,
            Color = createInfo.Color,
            Price = createInfo.Price,
            Condition = createInfo.Condition,
            Category = createInfo.Category
        };
    }

    public static Product ToEntity(this Product product, ProductUpdateInfo updateInfo)
    {
        product.Name = updateInfo.Name;
        product.Color = updateInfo.Color;
        product.Price = updateInfo.Price;
        product.Condition = updateInfo.Condition;
        product.Category = updateInfo.Category;
        product.Version++;
        product.UpdatedAt = DateTimeOffset.UtcNow;
        return product;
    }
}