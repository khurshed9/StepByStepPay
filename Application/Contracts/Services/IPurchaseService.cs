namespace Application.Contracts.Services;

public interface IPurchaseService
{
    Task<string> CreateAsync(PurchaseCreateInfo createInfo);

}