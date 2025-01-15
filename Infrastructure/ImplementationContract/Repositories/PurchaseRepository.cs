namespace Infrastructure.ImplementationContract.Repositories;

public class PurchaseRepository<T>(DataContext context) 
    : GenericRepository<Purchase>(context), IPurchaseRepository;