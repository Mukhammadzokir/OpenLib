using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class BookAuthor : Auditable
{
    public long BookId { get; set; }
    public Book Book { get; set; }
    public long AuthorId { get; set; }
    public Author Author { get; set; }
}
