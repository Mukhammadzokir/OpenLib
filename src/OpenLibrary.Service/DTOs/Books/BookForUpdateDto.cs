namespace OpenLibrary.Service.DTOs.Books;

public class BookForUpdateDto
{
    public string ISBN { get; set; }
    public long PublisherId { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
}
