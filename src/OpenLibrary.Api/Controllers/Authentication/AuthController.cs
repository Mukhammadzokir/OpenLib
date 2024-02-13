using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.DTOs.Logins;
using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Api.Controllers.Authentication;

public class AuthController : BaseController
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginForCreationDto dto)
        => Ok(await _service.AuthenticateAsync(dto));

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePasswordAsync(string email, string password)
        => Ok(new { Result = await _service.ChangePassword(email, password) });

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] StudentForCreationDto model)
        => Ok(await _service.CreateAsync(model));
}
