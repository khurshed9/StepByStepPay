namespace Application.DTO_s;

public interface IBasePurchaseInfo
{
    public int ProductId { get; init; }

    public int InstallmentMonths { get; init; }
    
    public int PhoneNumber { get; init; }
    
}

public record class PurchaseReadInfo(
    int ProductId,
    int InstallmentMonths,
    int PhoneNumber,
    int Id) : IBasePurchaseInfo;
    
    
public record class PurchaseCreateInfo(
    int ProductId,
    int InstallmentMonths,
    int PhoneNumber) : IBasePurchaseInfo;
    
    
public record class PurchaseUpdateInfo(
    int ProductId,
    int InstallmentMonths,
    int PhoneNumber) : IBasePurchaseInfo;