using OpenLibrary.Domain.Roles;
using OpenLibrary.Service.DTOs.UserCodes;
using OpenLibrary.Service.DTOs.ReturnedBooks;
using OpenLibrary.Service.DTOs.BorrowingRecords;

namespace OpenLibrary.Service.DTOs.Students;

public class StudentForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
    public bool IsVerified { get; set; } = false;
    public ICollection<UserCodeForResultDto> UserCodes { get; set; }
    public ICollection<ReturnedBookForResultDto> ReturnedBooks { get; set; }
    public ICollection<BorrowingRecordForResultDto> BorrowingRecords { get; set; }
}
