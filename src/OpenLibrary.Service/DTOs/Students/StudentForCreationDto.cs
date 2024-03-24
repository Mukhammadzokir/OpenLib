using System.ComponentModel.DataAnnotations;
using OpenLibrary.Service.Commons.Attributes;

namespace OpenLibrary.Service.DTOs.Students;

public class StudentForCreationDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    public string Phone {  get; set; }
    
    [EmailAttribute]
    public string Email { get; set; }

    [StrongPasswordAttribute]
    public string Password { get; set; }
    public string? Address { get; set; }
}
