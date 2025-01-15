namespace Application.Contracts.Repositories.BaseRepositories.CRUD;

public interface IGenericUpdateRepository<T> where T : BaseEntity
{
    Task<Result<int>> UpdateAsync(T value);
}