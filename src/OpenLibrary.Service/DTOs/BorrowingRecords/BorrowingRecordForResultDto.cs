using OpenLibrary.Domain.Entities;

namespace OpenLibrary.Service.DTOs.BorrowingRecords;

public class BorrowingRecordForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
    public DateTime BorrowingDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Fine { get; set; }
}
