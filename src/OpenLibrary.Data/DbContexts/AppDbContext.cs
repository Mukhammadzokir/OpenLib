using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OpenLibrary.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BorrowingRecord> BorrowingRecords { get; set; }
}