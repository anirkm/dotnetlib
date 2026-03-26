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
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_catalogManager.GetCatalog());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetBook(int id)
    {
        Book? book = _catalogManager.FindBook(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet("type/{type}")]
    public ActionResult<IEnumerable<Book>> GetBooksByType(TypeBook type)
    {
        return Ok(_catalogManager.GetCatalog(type));
    }

    [HttpGet("top-rated")]
    public ActionResult<Book> GetTopRatedBook()
    {
        Book? book = _catalogManager.GetTopRatedBook();

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> AddBook([FromBody] Book book)
    {
        Book createdBook = _catalogManager.AddBook(book);
        Book? createdBookWithRelations = _catalogManager.FindBook(createdBook.Id);

        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBookWithRelations ?? createdBook);
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
}
