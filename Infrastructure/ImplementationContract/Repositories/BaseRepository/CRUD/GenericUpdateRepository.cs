namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.CRUD;

public class GenericUpdateRepository<T>(DataContext context) : IGenericUpdateRepository<T> where T : BaseEntity
{
    public async Task<Result<int>> UpdateAsync(T value)
    {
        try
        {
            T? existing = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == value.Id);
            if (existing == null)
                return Result<int>.Failure(Error.NotFound());

            context.Entry(existing).CurrentValues.SetValues(value);

            if (context.Entry(existing).State == EntityState.Unchanged)
                return Result<int>.Success(1);

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