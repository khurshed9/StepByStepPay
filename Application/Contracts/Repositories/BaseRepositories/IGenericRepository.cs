namespace Application.Contracts.Repositories.BaseRepositories;

public interface IGenericRepository<T> : 
    IGenericAddRepository<T>,
    IGenericDeleteRepository<T>,
    IGenericFindRepository<T>,
    IGenericUpdateRepository<T> where T : BaseEntity;