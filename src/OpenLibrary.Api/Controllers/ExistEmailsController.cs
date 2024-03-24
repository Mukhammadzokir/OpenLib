using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;

namespace OpenLibrary.Api.Controllers.Authentication;

public class ExistEmailsController : BaseController
{
    private readonly IExistEmailService _service;

    public ExistEmailsController(
        IExistEmailService service)
    {
        _service = service;
    }

    [HttpPost("Check")]
    public async Task<IActionResult> CheckEmail(string email)
    {
        var result = await _service.EmailExistance(email);
        return Ok(new
        {
            result = result.ToString(),
            resultCode = (int)result
        });
    }

    [HttpPost("Verify/Code")]
    public async Task<IActionResult> VerifyCode(string email, long code)
    {
        var res = await _service.VerifyCodeAsync(email, code);
        return Ok(new
        {
            verified = res
        });
    }
}
