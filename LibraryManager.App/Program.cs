using System;
using System.Collections.Generic;

namespace LibraryManager.App;

public static class Program
{
    public static void Main(string[] args)
    {
        // Seed a few books before wiring a real data source.
        List<Book> books = new()
        {
            new Book { Name = "The Count of Monte Crypto", Type = "Aventure" },
            new Book { Name = "C# for Wizards Who Fear Semicolons", Type = "Teaching" },
            new Book { Name = "Lord of the Strings: Return of the Null", Type = "Fantasy" },
            new Book { Name = "How to Debug Your Cat in 24 Easy Steps", Type = "Comedy" }
        };

        // Display every title currently available in the catalog.
        foreach (Book book in books)
        {
            Console.WriteLine(book.Name);
        }
    }
}
