using System.Collections.Generic;

namespace BusinessObjects.Entity;

public class Author : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();
}
