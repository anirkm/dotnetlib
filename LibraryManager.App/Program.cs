using System;
using System.Collections.Generic;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace LibraryManager.App;

public static class Program
{
    public static void Main(string[] args)
    {
        BookRepository bookRepository = new();
        IEnumerable<Book> books = bookRepository.GetAll();
        Book? featuredBook = bookRepository.Get(1);
        IEnumerable<Book> adventureBooks = books.Where(book => book.Type == TypeBook.Aventure);

        if (featuredBook is not null)
        {
            Console.WriteLine($"Featured book: {featuredBook.Name}");
            Console.WriteLine();
        }

        foreach (Book book in adventureBooks)
        {
            Console.WriteLine(book.Name);
        }
    }
}
