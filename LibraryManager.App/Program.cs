using System;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Services;

namespace LibraryManager.App;

public static class Program
{
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder();
        using IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider services = serviceScope.ServiceProvider;
        ICatalogManager catalogManager = services.GetRequiredService<ICatalogManager>();

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

    private static IHost CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IGenericRepository<Book>, BookRepository>();
                services.AddTransient<IGenericRepository<Author>, AuthorRepository>();
                services.AddTransient<IGenericRepository<Library>, LibraryRepository>();
                services.AddTransient<ICatalogManager, CatalogManager>();
            })
            .Build();
    }
}
