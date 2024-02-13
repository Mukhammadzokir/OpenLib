using OpenLibrary.Domain.Entities;
using OpenLibrary.Service.DTOs.BookAuthors;
using OpenLibrary.Service.DTOs.BorrowingRecords;

namespace OpenLibrary.Service.DTOs.Books;

public class BookForResultDto
{
    public long Id { get; set; }
    public string ISBN { get; set; }
    public Publisher Publisher { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
    public ICollection<BookAuthorForResultDto> Authors { get; set; }
    public ICollection<BorrowingRecordForResultDto> BorrowingRecords { get; set; }
}
