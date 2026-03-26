namespace BusinessObjects.DataTransferObject;

public class BookAuthorDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Pages { get; set; }

    public string Type { get; set; } = string.Empty;

    public AuthorDto? Author { get; set; }

    public List<LibraryDto> Libraries { get; set; } = [];
}
