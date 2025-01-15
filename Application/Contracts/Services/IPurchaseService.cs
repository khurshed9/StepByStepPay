namespace Application.Contracts.Services;

public interface IPurchaseService
{
    Task<Result<PagedResponse<IEnumerable<PurchaseReadInfo>>>> GetAllAsync(PurchaseFilter filter);

    Task<string> CreateAsync(PurchaseCreateInfo createInfo);

    Task<BaseResult> DeleteAsync(int id);
}