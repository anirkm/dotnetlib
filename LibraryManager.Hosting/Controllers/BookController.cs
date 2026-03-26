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
            Author = book.Author is null
                ? null
                : new AuthorDto
                {
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                }
        };
    }
}
