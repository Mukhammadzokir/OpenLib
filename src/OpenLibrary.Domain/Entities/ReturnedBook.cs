using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class ReturnedBook : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BookId { get; set; }
    public DateTime ReturnedDate { get; set; } = DateTime.UtcNow;
    public decimal Fine { get; set; }
}
