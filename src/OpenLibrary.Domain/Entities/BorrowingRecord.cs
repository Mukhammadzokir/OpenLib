using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class BorrowingRecord : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
    public DateTime BorrowingDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Fine { get; set; }
}
