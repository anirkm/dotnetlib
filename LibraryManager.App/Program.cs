using System;
using BusinessObjects.Enum;
using Services.Services;

namespace LibraryManager.App;

public static class Program
{
    public static void Main(string[] args)
    {
        CatalogManager catalogManager = new();
        var catalog = catalogManager.GetCatalog();
        var featuredBook = catalogManager.FindBook(1);
        var adventureBooks = catalogManager.GetCatalog(TypeBook.Aventure);

        Console.WriteLine("Full catalog:");

        foreach (var book in catalog)
        {
            Console.WriteLine(book.Name);
        }

        Console.WriteLine();

        if (featuredBook is not null)
        {
            Console.WriteLine($"Featured book: {featuredBook.Name}");
            Console.WriteLine();
        }

        Console.WriteLine("Adventure books:");

        foreach (var book in adventureBooks)
        {
            Console.WriteLine(book.Name);
        }
    }
}
