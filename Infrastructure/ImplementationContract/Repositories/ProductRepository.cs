namespace Infrastructure.ImplementationContract.Repositories;

public class ProductRepository<T>(DataContext context) 
    : GenericRepository<Product>(context), IProductRepository;