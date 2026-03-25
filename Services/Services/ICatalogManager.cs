using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace Services.Services;

public interface ICatalogManager
{
    IEnumerable<Book> GetCatalog();

    IEnumerable<Book> GetCatalog(TypeBook type);

    Book? FindBook(int id);
}
