namespace OpenLibrary.Service.DTOs.Books;

public class BookForCreationDto
{
    public long PublisherId { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
}
