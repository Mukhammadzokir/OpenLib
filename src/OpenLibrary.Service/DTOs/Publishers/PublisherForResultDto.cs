using OpenLibrary.Service.DTOs.Books;

namespace OpenLibrary.Service.DTOs.Publishers;

public class PublisherForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ICollection<BookForResultDto> Books { get; set; }

}
