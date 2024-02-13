using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class Book : Auditable
{
    public string ISBN { get; set; }
    public long PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
    public ICollection<BookAuthor> Authors { get; set; }
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; }
}
