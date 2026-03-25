using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services;

public class CatalogManager : ICatalogManager
{
    private readonly IGenericRepository<Book> _bookRepository;

    public CatalogManager(IGenericRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<Book> GetCatalog()
    {
        return _bookRepository.GetAll();
    }

    public IEnumerable<Book> GetCatalog(TypeBook type)
    {
        return _bookRepository.GetAll().Where(book => book.Type == type);
    }

    public Book? FindBook(int id)
    {
        return _bookRepository.Get(id);
    }
}
