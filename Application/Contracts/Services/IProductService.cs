namespace Application.Contracts.Services;

public interface IProductService
{
    Task<Result<PagedResponse<IEnumerable<ProductReadInfo>>>> GetAllAsync(ProductFilter filter);
    
    Task<Result<ProductReadInfo>> GetByIdAsync(int id);

    Task<BaseResult> CreateAsync(ProductCreateInfo product);
    
    Task<BaseResult> UpdateAsync(int id, ProductUpdateInfo product);
    
    Task<BaseResult> DeleteAsync(int id);
}