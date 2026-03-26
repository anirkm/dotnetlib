using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace Services.Services;

public interface ICatalogManager
{
    IEnumerable<Book> GetCatalog();

    IEnumerable<Book> GetCatalog(TypeBook type);

    IEnumerable<Book> GetFilteredBooks(TypeBook? type = null, string? authorFirstName = null, string? authorLastName = null);

    Book? FindBook(int id);

    Book AddBook(Book book);

    Book? GetTopRatedBook();

    bool DeleteBook(int id);

    Author? FindAuthor(string firstName, string lastName);
}
