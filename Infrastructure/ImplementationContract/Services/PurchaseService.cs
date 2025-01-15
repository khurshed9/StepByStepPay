namespace Infrastructure.ImplementationContract.Services;

public class PurchaseService(IPurchaseRepository repository,DataContext context) : IPurchaseService
{
     public async Task<string> CreateAsync(PurchaseCreateInfo createInfo)
    {
        Product? p = await context.Products.FirstOrDefaultAsync(x => x.Id == createInfo.ProductId);
        
        if(p == null)
            return $"Product with {createInfo.ProductId} ID doesn't exist";                           
 
        decimal totalAmount = p.Price;
        decimal perMonth = Math.Round(p.Price / createInfo.InstallmentMonths, 2);
        string message = $"{p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\n" +
                         $"PerMonth: {perMonth}\n" +
                         $"Category: {p.Category}\nPhoneNumber: {createInfo.PhoneNumber}";
        
        if (p?.Category == Category.SmartPhone)
        {
            if (createInfo.InstallmentMonths > 9)
            {
                int extraMonth = createInfo.InstallmentMonths - 9;
                decimal percentage = extraMonth * 3;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"{Category.SmartPhone}: {message}\nTotalAmount: {totalAmount}";
            }
            else
                return $"{Category.SmartPhone}: {message}\nTotalAmount: {totalAmount}";
        }
        
        else if (p?.Category == Category.Computer)
        {
            if (createInfo.InstallmentMonths > 12)
            {
                int extraMonth = createInfo.InstallmentMonths - 12;
                decimal percentage = extraMonth * 4;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"{Category.Computer}: {message}\nTotalAmount: {totalAmount}";
            }
            else
                return $"{Category.Computer}: {message}\nTotalAmount: {totalAmount}";
        }

        else if (p?.Category == Category.Television)
        {
            if (createInfo.InstallmentMonths > 18)
            {
                int extraMonth = createInfo.InstallmentMonths - 18;
                decimal percentage = extraMonth * 5;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"{Category.Television}: {message}\nTotalAmount: {totalAmount}";
            }
            else
                return $"{Category.Television}: {message}\nTotalAmount: {totalAmount}";
        }
        await repository.AddAsync(createInfo.ToEntity());

        return "Invalid product category.";
    }

     
}