using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Domain.Entities;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; }
}
