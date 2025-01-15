namespace Infrastructure.ImplementationContract.Services;

public class PurchaseService(IPurchaseRepository repository,DataContext context) : IPurchaseService
{
    public async Task<Result<PagedResponse<IEnumerable<PurchaseReadInfo>>>> GetAllAsync(PurchaseFilter filter)
    {
        return await Task.Run(() =>
        {
            Expression<Func<Purchase, bool>> filterExpression = purchase =>
                (filter.ProductId == null || purchase.ProductId == filter.ProductId) &&
                (filter.InstallmentMonths == null || purchase.InstallmentMonths == filter.InstallmentMonths) &&
                (filter.PhoneNumber == null || purchase.PhoneNumber == filter.PhoneNumber);

            Result<IQueryable<Purchase>> request = repository
                .Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<PurchaseReadInfo>>>.Failure(request.Error);

            List<PurchaseReadInfo> query = request.Value!.Select(x => x.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<PurchaseReadInfo> purchase =
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<PurchaseReadInfo>> res =
                PagedResponse<IEnumerable<PurchaseReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, purchase);

            return Result<PagedResponse<IEnumerable<PurchaseReadInfo>>>.Success(res);
        });
    }
    
     public async Task<string> CreateAsync(PurchaseCreateInfo createInfo)
    {
        Product? p = await context.Products.FirstOrDefaultAsync(x => x.Id == createInfo.ProductId);
        
        if(p == null)
            return $"Product with {createInfo.ProductId} ID doesn't exist";

        decimal totalAmount = p.Price;
        decimal perMonth = Math.Round(p.Price / createInfo.InstallmentMonths, 2);
        if (p?.Category == Category.SmartPhone)
        {
            if (createInfo.InstallmentMonths > 9)
            {
                int extraMonth = createInfo.InstallmentMonths - 9;
                decimal percentage = extraMonth * 3;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"SmartPhone: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPerMonth: {perMonth}";
            }
            else
            {
                return $"SmartPhone: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPerMonth: {perMonth}";
            }
        }
        
        else if (p?.Category == Category.Computer)
        {
            if (createInfo.InstallmentMonths > 12)
            {
                int extraMonth = createInfo.InstallmentMonths - 12;
                decimal percentage = extraMonth * 4;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"Computer: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPer Month: {perMonth}";
            }
            else
            {
                return $"Computer: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPer Month: {perMonth}";
            }
        }

        else if (p?.Category == Category.Television)
        {
            if (createInfo.InstallmentMonths > 18)
            {
                int extraMonth = createInfo.InstallmentMonths - 18;
                decimal percentage = extraMonth * 5;
                totalAmount = totalAmount + (totalAmount * percentage / 100);
                return $"Television: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPer Month: {perMonth}";
            }
            else
            {
                return $"Television: {p.Name}\nInstallmentMonths: {createInfo.InstallmentMonths}\nPhoneNumber: {createInfo.PhoneNumber}\nTotalAmount: {totalAmount}\nCategory: {p.Category}\nPer Month: {perMonth}";
            }
        }
        await repository.AddAsync(createInfo.ToEntity());

        return "Invalid product category.";
    }


    public async Task<BaseResult> DeleteAsync(int id)
    {
        Result<Purchase?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());
        
        Result<int> result = await repository.DeleteAsync(id);
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }
}