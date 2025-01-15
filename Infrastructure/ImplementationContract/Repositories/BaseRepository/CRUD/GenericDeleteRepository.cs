namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.CRUD;

public class GenericDeleteRepository<T>(DataContext context) : IGenericDeleteRepository<T> where T : BaseEntity
{
    public async Task<Result<int>> DeleteAsync(int id)
    {
        try
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
            if (entity == null)
                return Result<int>.Failure(Error.NotFound());

            context.Set<T>().Update((T)entity.ToDelete());
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

    public async Task<Result<int>> DeleteAsync(T value)
    {
        try
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == value.Id);
            if (entity == null)
                return Result<int>.Failure(Error.NotFound());

            context.Set<T>().Update((T)entity.ToDelete());
            int res = await context.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(Error.InternalServerError(ex.Message));
        }
    }
}