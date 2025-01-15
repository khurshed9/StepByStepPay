namespace Application.Contracts.Repositories.BaseRepositories.CRUD;

public interface IGenericAddRepository<T> where T : BaseEntity
{
    Task<Result<int>> AddAsync(T value);
    
    Task<Result<int>> AddRangeAsync(IEnumerable<T> values);
}