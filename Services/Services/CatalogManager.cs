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
        return _bookRepository.GetMultiple(null, nameof(Book.Author), nameof(Book.Libraries));
    }

    public IEnumerable<Book> GetCatalog(TypeBook type)
    {
        return _bookRepository.GetMultiple(book => book.Type == type, nameof(Book.Author), nameof(Book.Libraries));
    }

    public Book? FindBook(int id)
    {
        return _bookRepository.GetMultiple(book => book.Id == id, nameof(Book.Author), nameof(Book.Libraries))
            .FirstOrDefault();
    }
}
