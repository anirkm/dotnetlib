using System;
using BusinessObjects.Enum;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        var lawBook = catalogManager.GetCatalog(TypeBook.Juridique);
        var teachingBooks = catalogManager.GetCatalog(TypeBook.Enseignement);

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

        Console.WriteLine("Law books:");

        foreach (var book in lawBook)
        {
            Console.WriteLine(book.Name);
        }

        Console.WriteLine("Teaching books:");

        foreach (var book in teachingBooks)
        {
            Console.WriteLine(book.Name);
        }

        Console.WriteLine();
    }

    private static IHost CreateHostBuilder()
    {
        string dbPath = Path.Combine(AppContext.BaseDirectory, "library.db");

        return Host.CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Warning);
            })
            .ConfigureServices(services =>
            {
                services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlite($"Data Source={dbPath}");
                });

                services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddTransient<ICatalogManager, CatalogManager>();
            })
            .Build();
    }
}
