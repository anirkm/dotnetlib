using BusinessObjects.DataTransferObject;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryManager.Hosting.Controllers;

[ApiController]
[Route("books")]
public class BookController : ControllerBase
{
    private readonly ICatalogManager _catalogManager;

    public BookController(ICatalogManager catalogManager)
    {
        _catalogManager = catalogManager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookDto>> GetBooks()
    {
        return Ok(_catalogManager.GetCatalog().Select(ToDto));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BookDto> GetBook(int id)
    {
        Book? book = _catalogManager.FindBook(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(ToDto(book));
    }

    [HttpGet("type/{type}")]
    public ActionResult<IEnumerable<BookDto>> GetBooksByType(TypeBook type)
    {
        return Ok(_catalogManager.GetCatalog(type).Select(ToDto));
    }

    [HttpGet("top-rated")]
    public ActionResult<BookDto> GetTopRatedBook()
    {
        Book? book = _catalogManager.GetTopRatedBook();

        if (book is null)
        {
            return NotFound();
        }

        return Ok(ToDto(book));
    }

    [HttpGet("/filteredBooks")]
    public ActionResult<IEnumerable<BookAuthorDto>> GetFilteredBooks(
        [FromQuery] string? type,
        [FromQuery] string? authorFirstName,
        [FromQuery] string? authorLastName)
    {
        if (string.IsNullOrWhiteSpace(type) &&
            string.IsNullOrWhiteSpace(authorFirstName) &&
            string.IsNullOrWhiteSpace(authorLastName))
        {
            return BadRequest("At least one filter is required.");
        }

        TypeBook? parsedType = null;

        if (!string.IsNullOrWhiteSpace(type))
        {
            if (!Enum.TryParse(type, true, out TypeBook typeBook))
            {
                return BadRequest("Unknown book type.");
            }

            parsedType = typeBook;
        }

        IEnumerable<BookAuthorDto> filteredBooks = _catalogManager
            .GetFilteredBooks(parsedType, authorFirstName, authorLastName)
            .Select(ToBookAuthorDto);

        return Ok(filteredBooks);
    }

    [HttpPost]
    public ActionResult<BookDto> AddBook([FromBody] BookCreateDto bookCreateDto)
    {
        if (!Enum.TryParse(bookCreateDto.Type, true, out TypeBook type))
        {
            return BadRequest("Unknown book type.");
        }

        Author? author = _catalogManager.FindAuthor(bookCreateDto.AuthorFirstName, bookCreateDto.AuthorLastName);

        if (author is null)
        {
            return BadRequest("Author not found.");
        }

        Book book = new()
        {
            Name = bookCreateDto.Name,
            Pages = bookCreateDto.Pages,
            Type = type,
            Rate = 0,
            AuthorId = author.Id
        };

        Book createdBook = _catalogManager.AddBook(book);
        Book? createdBookWithRelations = _catalogManager.FindBook(createdBook.Id);

        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, ToDto(createdBookWithRelations ?? createdBook));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        bool deleted = _catalogManager.DeleteBook(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static BookDto ToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Pages = book.Pages,
            Type = book.Type.ToString(),
            Author = ToAuthorDto(book.Author)
        };
    }

    private static BookAuthorDto ToBookAuthorDto(Book book)
    {
        return new BookAuthorDto
        {
            Id = book.Id,
            Name = book.Name,
            Pages = book.Pages,
            Type = book.Type.ToString(),
            Author = ToAuthorDto(book.Author),
            Libraries = book.Libraries
                .Select(library => new LibraryDto
                {
                    Name = library.Name,
                    Address = library.Address
                })
                .ToList()
        };
    }

    private static AuthorDto? ToAuthorDto(Author? author)
    {
        return author is null
            ? null
            : new AuthorDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };
    }
}
