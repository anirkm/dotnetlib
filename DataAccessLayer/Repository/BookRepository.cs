using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace DataAccessLayer.Repository;

public class BookRepository
{
    private readonly List<Book> _books;

    public BookRepository()
    {
        Author alexandreDumas = new()
        {
            Id = 1,
            FirstName = "Alexandre",
            LastName = "Dumas"
        };

        Author remySynave = new()
        {
            Id = 2,
            FirstName = "Remy",
            LastName = "Synave"
        };

        _books =
        [
            new Book
            {
                Id = 1,
                Name = "The Count of Monte Crypto",
                Pages = 900,
                Type = TypeBook.Aventure,
                Rate = 10,
                AuthorId = alexandreDumas.Id,
                Author = alexandreDumas
            },
            new Book
            {
                Id = 2,
                Name = "Indiana Jones and the Singleton of Doom",
                Pages = 320,
                Type = TypeBook.Aventure,
                Rate = 9,
                AuthorId = alexandreDumas.Id,
                Author = alexandreDumas
            },
            new Book
            {
                Id = 3,
                Name = "C# for Wizards Who Fear Semicolons",
                Pages = 480,
                Type = TypeBook.Enseignement,
                Rate = 8,
                AuthorId = remySynave.Id,
                Author = remySynave
            },
            new Book
            {
                Id = 4,
                Name = "GDPR and Other Bedtime Stories",
                Pages = 250,
                Type = TypeBook.Juridique,
                Rate = 7,
                AuthorId = alexandreDumas.Id,
                Author = alexandreDumas
            }
        ];
    }

    public IEnumerable<Book> GetAll()
    {
        return _books;
    }

    public Book? Get(int id)
    {
        return _books.FirstOrDefault(book => book.Id == id);
    }
}
