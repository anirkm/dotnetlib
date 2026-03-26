using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly LibraryContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(LibraryContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? Get(int id)
    {
        return _dbSet.FirstOrDefault(entity => entity.Id == id);
    }

    public T Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (string include in includes)
        {
            query = query.Include(include);
        }

        if (filter is not null)
        {
            return query.AsEnumerable().Where(filter).ToList();
        }

        return query.ToList();
    }
}
