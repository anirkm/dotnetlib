using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services;

public class CatalogManager
{
    private readonly BookRepository _bookRepository;

    public CatalogManager()
    {
        _bookRepository = new BookRepository();
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
