using OpenLibrary.Service.DTOs.Logins;
using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Service.Interfaces;

public interface IAuthService
{
    public Task<LoginForResultDto> AuthenticateAsync(LoginForCreationDto login);
    public Task<LoginForResultDto> CreateAsync(StudentForCreationDto user);
    public Task<bool> ChangePassword(string email, string password);

}
