using OpenLibrary.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Domain.Entities;

public class UserCode : Auditable
{
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public Student User { get; set; }

    [MaxLength(10)]
    public long Code { get; set; }

    public DateTime ExpireDate { get; set; }
}
