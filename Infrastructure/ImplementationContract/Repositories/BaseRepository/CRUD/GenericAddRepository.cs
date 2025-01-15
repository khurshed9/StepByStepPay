namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.CRUD;

public class GenericAddRepository<T>(DataContext context) : IGenericAddRepository<T> where T : BaseEntity
{
    public async Task<Result<int>> AddAsync(T value)
    {
        try
        {
            await context.Set<T>().AddAsync(value);
            int res = await context.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<int>> AddRangeAsync(IEnumerable<T> values)
    {
        try
        {
            await context.Set<T>().AddRangeAsync(values);
            int res = await context.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }
}