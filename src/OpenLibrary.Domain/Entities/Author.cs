using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class Author : Auditable
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public ICollection<BookAuthor> Books { get; set; }
}
