using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Service.Interfaces;

public interface IJwtTokenService
{
    Task<(string token, DateTime tokenExpiryTime)> GenerateTokenAsync(StudentForResultDto user);
    Task<(string refreshToken, DateTime tokenValidityTime)> GenerateRefreshTokenAsync();
    Task<StudentForResultDto?> GetUserByAccessTokenAsync(string accessToken);
}
