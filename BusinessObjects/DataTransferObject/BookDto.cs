namespace BusinessObjects.DataTransferObject;

public class BookDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Pages { get; set; }

    public string Type { get; set; } = string.Empty;

    public AuthorDto? Author { get; set; }
}
