using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Entity;

namespace DataAccessLayer.Repository;

public class AuthorRepository
{
    private readonly List<Author> _authors;

    public AuthorRepository()
    {
        _authors =
        [
            new Author { Id = 1, FirstName = "Alexandre", LastName = "Dumas" },
            new Author { Id = 2, FirstName = "Remy", LastName = "Synave" },
            new Author { Id = 3, FirstName = "Dany", LastName = "Capitaine" },
            new Author { Id = 4, FirstName = "Severine", LastName = "Lettrez" }
        ];
    }

    public IEnumerable<Author> GetAll()
    {
        return _authors;
    }

    public Author? Get(int id)
    {
        return _authors.FirstOrDefault(author => author.Id == id);
    }
}
