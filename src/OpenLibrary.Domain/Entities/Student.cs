using OpenLibrary.Domain.Roles;
using OpenLibrary.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Domain.Entities;

public class Student : Auditable
{
    [MaxLength(20)]
    public string FirstName { get; set; }

    [MaxLength(20)]
    public string LastName { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    [MaxLength(30)]
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool IsVerified { get; set; } = false;
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public string? Address { get; set; }
    public ICollection<UserCode> UserCodes { get; set; }
    public ICollection<ReturnedBook> ReturnedBooks { get; set; }
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; }
}
