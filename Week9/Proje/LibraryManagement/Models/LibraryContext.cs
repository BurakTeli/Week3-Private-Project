using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                DateOfBirth = new DateTime(1980, 5, 20)
            },
            new Author
            {
                Id = 2,
                FirstName = "Elif",
                LastName = "Demir",
                DateOfBirth = new DateTime(1990, 11, 15)
            }
        );
    }
}
