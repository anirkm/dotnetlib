using System.Collections.Generic;
using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Library> Libraries => Set<Library>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("author");
            entity.HasKey(author => author.Id);
            entity.Property(author => author.Id).HasColumnName("id");
            entity.Property(author => author.FirstName).HasColumnName("firstName");
            entity.Property(author => author.LastName).HasColumnName("lastName");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.ToTable("library");
            entity.HasKey(library => library.Id);
            entity.Property(library => library.Id).HasColumnName("id");
            entity.Property(library => library.Name).HasColumnName("name");
            entity.Property(library => library.Address).HasColumnName("adress");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("book");
            entity.HasKey(book => book.Id);
            entity.Property(book => book.Id).HasColumnName("id");
            entity.Property(book => book.Name).HasColumnName("name");
            entity.Property(book => book.Pages).HasColumnName("pages");
            entity.Property(book => book.Type).HasColumnName("type").HasConversion<string>();
            entity.Property(book => book.Rate).HasColumnName("rate");
            entity.Property(book => book.AuthorId).HasColumnName("id_author");

            entity.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);

            entity.HasMany(book => book.Libraries)
                .WithMany(library => library.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "stock",
                    right => right.HasOne<Library>()
                        .WithMany()
                        .HasForeignKey("id_library"),
                    left => left.HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("id_book"),
                    join =>
                    {
                        join.ToTable("stock");
                        join.HasKey("id_book", "id_library");
                        join.IndexerProperty<int>("id_book").HasColumnName("id_book");
                        join.IndexerProperty<int>("id_library").HasColumnName("id_library");
                    });
        });
    }
}
