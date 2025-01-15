namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.CRUD;

public class GenericFindRepository<T>(DataContext context) : IGenericFindRepository<T> where T : BaseEntity
{
    public async Task<Result<T?>> GetByIdAsync(int id)
    {
        try
        {
            T? res = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return res != null
                ? Result<T?>.Success(res)
                : Result<T?>.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return Result<T?>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            return Result<IEnumerable<T>>
                .Success(await context.Set<T>()
                    .ToListAsync());
        }
        catch (Exception e)
        {
            return Result<IEnumerable<T>>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public Result<IQueryable<T>> Find(Expression<Func<T, bool>> expression)
    {
        try
        {
            return Result<IQueryable<T>>
                .Success(context.Set<T>()
                    .Where(expression).AsQueryable());
        }
        catch (Exception ex)
        {
            return Result<IQueryable<T>>.Failure(Error.InternalServerError(ex.Message));
        }
    }
}