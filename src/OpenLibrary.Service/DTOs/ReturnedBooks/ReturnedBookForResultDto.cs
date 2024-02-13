using OpenLibrary.Domain.Entities;

namespace OpenLibrary.Service.DTOs.ReturnedBooks;

public class ReturnedBookForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BookId { get; set; }
    public DateTime ReturnedDate { get; set; }
    public decimal Fine { get; set; }
}
