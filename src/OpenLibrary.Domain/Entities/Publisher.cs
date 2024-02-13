using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class Publisher : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ICollection<Book> Books { get; set; }
}

