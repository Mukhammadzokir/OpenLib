namespace OpenLibrary.Service.DTOs.BorrowingRecords;

public class BorrowingRecordForUpdateDto
{
    public long StudentId { get; set; }
    public long BookId { get; set; }
    public DateTime BorrowingDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Fine { get; set; }
}
