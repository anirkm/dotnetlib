using BusinessObjects.Entity;

namespace DataAccessLayer.Repository;

public interface IGenericRepository<T> where T : IEntity
{
    IEnumerable<T> GetAll();

    T? Get(int id);

    T Add(T entity);

    bool Delete(int id);

    IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes);
}
