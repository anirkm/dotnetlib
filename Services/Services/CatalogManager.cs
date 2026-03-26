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

    public Book AddBook(Book book)
    {
        book.Id = 0;
        book.Author = null;
        book.Libraries = [];

        return _bookRepository.Add(book);
    }

    public Book? GetTopRatedBook()
    {
        return GetCatalog()
            .OrderByDescending(book => book.Rate)
            .FirstOrDefault();
    }

    public bool DeleteBook(int id)
    {
        return _bookRepository.Delete(id);
    }
}
