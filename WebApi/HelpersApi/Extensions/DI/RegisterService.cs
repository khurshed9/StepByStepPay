namespace WebApi.HelpersApi.Extensions.DI;

public static class RegisterService
{

    public static IServiceCollection AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        
        //swagger
        builder.Services.AddSwaggerGen();
        
        //registration controller
        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<DataContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            x.LogTo(Console.WriteLine);
        });
        
        //registration generic repository
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IGenericAddRepository<>), typeof(GenericAddRepository<>));
        builder.Services.AddScoped(typeof(IGenericUpdateRepository<>), typeof(GenericUpdateRepository<>));
        builder.Services.AddScoped(typeof(IGenericDeleteRepository<>), typeof(GenericDeleteRepository<>));
        builder.Services.AddScoped(typeof(IGenericFindRepository<>), typeof(GenericFindRepository<>));
        
        //registration repository
        builder.Services.AddScoped<IProductRepository, ProductRepository<Product>>();
        builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository<Purchase>>();
        
        //registration services
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IPurchaseService, PurchaseService>();
        
        return builder.Services;
    }

    public static async Task<WebApplication> UseMiddlewares(this WebApplication app)
    {
        try
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseExceptionHandler("/error");
            app.MapControllers();
            await app.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return app;
    }
}