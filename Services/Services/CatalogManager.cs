using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services;

public class CatalogManager : ICatalogManager
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Author> _authorRepository;

    public CatalogManager(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
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

    public Author? FindAuthor(string firstName, string lastName)
    {
        return _authorRepository.GetMultiple(author =>
                author.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                author.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
    }
}
