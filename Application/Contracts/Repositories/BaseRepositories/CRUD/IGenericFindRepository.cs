namespace Application.Contracts.Repositories.BaseRepositories.CRUD;

public interface IGenericFindRepository<T> where T : BaseEntity
{
    Task<Result<T?>> GetByIdAsync(int id);

    Task<Result<IEnumerable<T>>> GetAllAsync();

    Result<IQueryable<T>> Find(Expression<Func<T, bool>> expression);
}