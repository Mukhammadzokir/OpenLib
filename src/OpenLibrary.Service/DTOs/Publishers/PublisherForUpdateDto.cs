using OpenLibrary.Domain.Entities;

namespace OpenLibrary.Service.DTOs.Publishers;

public class PublisherForUpdateDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
