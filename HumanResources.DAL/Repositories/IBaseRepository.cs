namespace HumanResources.DAL.Repositories
{
    public interface IBaseRepository<T, TId>
    {
        IEnumerable<T> GetAll();
        T Get(TId id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(TId id);
    }
}
