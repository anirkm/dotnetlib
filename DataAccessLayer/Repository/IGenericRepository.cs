using BusinessObjects.Entity;

namespace DataAccessLayer.Repository;

public interface IGenericRepository<T> where T : IEntity
{
    IEnumerable<T> GetAll();

    T? Get(int id);
}
