namespace Infrastructure.ImplementationContract.Services;

public class ProductService(IProductRepository repository,DataContext context) : IProductService
{
    public async Task<Result<PagedResponse<IEnumerable<ProductReadInfo>>>> GetAllAsync(ProductFilter filter)
    {
        return await Task.Run(() =>
        {
            Expression<Func<Product, bool>> filterExpression = product =>
                (string.IsNullOrEmpty(filter.Name) || product.Name.ToLower().Contains(filter.Name.ToLower())) &&
                (filter.Price == null || product.Price > filter.Price) &&
                (string.IsNullOrEmpty(filter.Condition) ||
                 product.Condition.ToLower().Contains(filter.Condition.ToLower()));
            
            Result<IQueryable<Product>> request = repository
                .Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<ProductReadInfo>>>.Failure(request.Error);
            
            List<ProductReadInfo> query = request.Value!.Select(x => x.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<ProductReadInfo> product=
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<ProductReadInfo>> res =
                PagedResponse<IEnumerable<ProductReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, product);

            return Result<PagedResponse<IEnumerable<ProductReadInfo>>>.Success(res);
        });
    }

    public async Task<Result<ProductReadInfo>> GetByIdAsync(int id)
    {
        Result<Product?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return Result<ProductReadInfo>.Failure(res.Error);

        return Result<ProductReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<BaseResult> CreateAsync(ProductCreateInfo product)
    {
        if(product.Price <= 0)
            return BaseResult.Failure(Error.BadRequest("Price must be greater than zero."));

        Result<int> res = await repository.AddAsync(product.ToEntity());
        
        return res.IsSuccess
            ? BaseResult.Success()
             : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, ProductUpdateInfo product)
    {
        try
        {
            Product? existingProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        
            if(existingProduct is null)
                return BaseResult.Failure(Error.NotFound());
            
            context.Products.Update(existingProduct.ToUpdate(product));
            int res = await context.SaveChangesAsync();
        
            return res is 0
                ? BaseResult.Failure(Error.InternalServerError("Data not saved!!!"))
                : BaseResult.Success();
        }
        catch (Exception e)
        {
            return BaseResult.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        // Result<Product?> res = await repository.GetByIdAsync(id);
        // if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());

        // Result<int> result = await repository.DeleteAsync(id);
        // return result.IsSuccess
        //     ? BaseResult.Success()
        //     : BaseResult.Failure(result.Error);


        Product res = await context.Products.FirstOrDefaultAsync(x => x.Id == id); ;
        if (res is null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found");

        context.Products.Remove(res);
        int result = context.SaveChanges();
        return result == 0 ? BaseResult.Failure(Error.BadRequest()) : BaseResult.Success();
        
    }
}