using OpenLibrary.Domain.Entities;

namespace OpenLibrary.Service.DTOs.BookAuthors;

public class BookAuthorForResultDto
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
    public long AuthorId { get; set; }
    public Author Author { get; set; }
}
