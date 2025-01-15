namespace Application.Contracts.Repositories.BaseRepositories.CRUD;

public interface IGenericDeleteRepository<T> where T : BaseEntity
{
    Task<Result<int>> DeleteAsync(int id);
    
    Task<Result<int>> DeleteAsync(T value);
}