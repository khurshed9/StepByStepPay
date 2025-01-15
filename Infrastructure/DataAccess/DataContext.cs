namespace Infrastructure.DataAccess;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Purchase> Purchases { get; set; }
}