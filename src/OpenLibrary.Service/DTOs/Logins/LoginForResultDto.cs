using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Service.DTOs.Logins;

public class LoginForResultDto
{
    public string Token { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
    public string RefreshToken { get; set; }
    public StudentForResultDto User { get; set; }
}
