using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;
using Moq;
using Services.Services;

namespace Services.Test;

public class CatalogManagerTest
{
    [Fact]
    public void GetCatalog_ReturnsAllBooks()
    {
        List<Book> books =
        [
            CreateBook(1, "The Count of Monte Crypto", TypeBook.Aventure),
            CreateBook(2, "C# for Wizards Who Fear Semicolons", TypeBook.Enseignement)
        ];

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(null, nameof(Book.Author), nameof(Book.Libraries)))
            .Returns(books);

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        IEnumerable<Book> result = catalogManager.GetCatalog();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, book => book.Name == "The Count of Monte Crypto");
        repositoryMock.Verify(
            repository => repository.GetMultiple(null, nameof(Book.Author), nameof(Book.Libraries)),
            Times.Once);
    }

    [Fact]
    public void GetCatalog_WithType_ReturnsFilteredBooks()
    {
        List<Book> books =
        [
            CreateBook(1, "The Count of Monte Crypto", TypeBook.Aventure),
            CreateBook(2, "Les trois mousquetaires", TypeBook.Aventure)
        ];

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)))
            .Returns((Func<Book, bool>? filter, string[] _) => filter is null ? [] : books.Where(filter).ToList());

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        IEnumerable<Book> result = catalogManager.GetCatalog(TypeBook.Aventure);

        Assert.Equal(2, result.Count());
        Assert.All(result, book => Assert.Equal(TypeBook.Aventure, book.Type));
        repositoryMock.Verify(
            repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)),
            Times.Once);
    }

    [Fact]
    public void FindBook_WithExistingId_ReturnsBook()
    {
        Book expectedBook = CreateBook(1, "Le conte de Monte Cristo", TypeBook.Aventure);

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)))
            .Returns((Func<Book, bool>? filter, string[] _) =>
            {
                List<Book> books = [expectedBook];
                return filter is null ? books : books.Where(filter).ToList();
            });

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        Book? result = catalogManager.FindBook(1);

        Assert.NotNull(result);
        Assert.Equal(expectedBook.Name, result.Name);
        Assert.Equal(expectedBook.Id, result.Id);
    }

    [Fact]
    public void FindBook_WithUnknownId_ReturnsNull()
    {
        List<Book> books =
        [
            CreateBook(1, "Le conte de Monte Cristo", TypeBook.Aventure)
        ];

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)))
            .Returns((Func<Book, bool>? filter, string[] _) => filter is null ? books : books.Where(filter).ToList());

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        Book? result = catalogManager.FindBook(99);

        Assert.Null(result);
    }

    [Fact]
    public void GetFilteredBooks_WithTypeAndAuthorFilters_ReturnsMatchingBooks()
    {
        List<Book> books =
        [
            CreateBook(1, "Le conte de Monte Cristo", TypeBook.Aventure, "Alexandre", "Dumas"),
            CreateBook(2, "Les trois mousquetaires", TypeBook.Aventure, "Alexandre", "Dumas"),
            CreateBook(3, "Le RC Lens, un club pas comme les autres", TypeBook.Histoire, "Severine", "Lettrez")
        ];

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)))
            .Returns((Func<Book, bool>? filter, string[] _) => filter is null ? books : books.Where(filter).ToList());

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        IEnumerable<Book> result = catalogManager.GetFilteredBooks(TypeBook.Aventure, "alex", "dum");

        Assert.Equal(2, result.Count());
        Assert.All(result, book =>
        {
            Assert.Equal(TypeBook.Aventure, book.Type);
            Assert.NotNull(book.Author);
            Assert.Contains("Alex", book.Author!.FirstName, StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public void GetFilteredBooks_WithLastNameOnly_ReturnsMatchingBooks()
    {
        List<Book> books =
        [
            CreateBook(1, "Le conte de Monte Cristo", TypeBook.Aventure, "Alexandre", "Dumas"),
            CreateBook(2, "Apprendre le Java", TypeBook.Enseignement, "Remy", "Synave")
        ];

        Mock<IGenericRepository<Book>> repositoryMock = new();
        repositoryMock
            .Setup(repository => repository.GetMultiple(It.IsAny<Func<Book, bool>>(), nameof(Book.Author), nameof(Book.Libraries)))
            .Returns((Func<Book, bool>? filter, string[] _) => filter is null ? books : books.Where(filter).ToList());

        Mock<IGenericRepository<Author>> authorRepositoryMock = new();

        CatalogManager catalogManager = new(repositoryMock.Object, authorRepositoryMock.Object);

        IEnumerable<Book> result = catalogManager.GetFilteredBooks(authorLastName: "syn");

        Book filteredBook = Assert.Single(result);
        Assert.Equal("Apprendre le Java", filteredBook.Name);
    }

    private static Book CreateBook(int id, string name, TypeBook type, string authorFirstName = "Alexandre", string authorLastName = "Dumas")
    {
        return new Book
        {
            Id = id,
            Name = name,
            Type = type,
            Author = new Author
            {
                FirstName = authorFirstName,
                LastName = authorLastName
            }
        };
    }
}
