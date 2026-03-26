namespace BusinessObjects.DataTransferObject;

public class BookCreateDto
{
    public string Name { get; set; } = string.Empty;

    public int Pages { get; set; }

    public string Type { get; set; } = string.Empty;

    public string AuthorFirstName { get; set; } = string.Empty;

    public string AuthorLastName { get; set; } = string.Empty;
}
