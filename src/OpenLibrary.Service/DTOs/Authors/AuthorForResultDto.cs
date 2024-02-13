using OpenLibrary.Service.DTOs.BookAuthors;

namespace OpenLibrary.Service.DTOs.Authors;

public class AuthorForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public ICollection<BookAuthorForResultDto> Books { get; set; }
}
