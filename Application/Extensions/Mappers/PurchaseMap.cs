namespace Application.Extensions.Mappers;

public static class PurchaseMap
{
    
    public static PurchaseReadInfo ToRead(this Purchase purchase)
    {
        return new PurchaseReadInfo(
            purchase.ProductId,
            purchase.InstallmentMonths,
            purchase.PhoneNumber,
            purchase.Id);
    }

    public static Purchase ToEntity(this PurchaseCreateInfo purchaseInfo)
    {
        return new Purchase()
        {
            ProductId = purchaseInfo.ProductId,
            InstallmentMonths = purchaseInfo.InstallmentMonths,
            PhoneNumber = purchaseInfo.PhoneNumber,
        };
    }
}