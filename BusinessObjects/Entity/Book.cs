using System.Collections.Generic;
using BusinessObjects.Enum;

namespace BusinessObjects.Entity;

public class Book : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Pages { get; set; }

    public TypeBook Type { get; set; }

    public int Rate { get; set; }

    public int AuthorId { get; set; }

    public Author? Author { get; set; }

    public List<Library> Libraries { get; set; } = new();
}
