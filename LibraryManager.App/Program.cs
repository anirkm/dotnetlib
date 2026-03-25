using System;
using System.Collections.Generic;

namespace LibraryManager.App;

public static class Program
{
    public static void Main(string[] args)
    {
        List<Book> books = new()
        {
            new Book { Name = "The Count of Monte Crypto", Type = "Aventure" },
            new Book { Name = "Indiana Jones and the Singleton of Doom", Type = "Aventure" },
            new Book { Name = "C# for Wizards Who Fear Semicolons", Type = "Teaching" },
            new Book { Name = "Lord of the Strings: Return of the Null", Type = "Fantasy" },
            new Book { Name = "How to Debug Your Cat in 24 Easy Steps", Type = "Comedy" }
        };

        // Filter the catalog before displaying the matching titles.
        IEnumerable<Book> adventureBooks = books.Where(book => book.Type == "Aventure");

        foreach (Book book in adventureBooks)
        {
            Console.WriteLine(book.Name);
        }
    }
}
