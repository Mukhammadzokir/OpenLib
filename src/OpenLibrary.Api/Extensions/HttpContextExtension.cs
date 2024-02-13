using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Api.Extensions;

public static class HttpContextExtension
{
    public static StudentForResultDto? GetUser(this HttpContext httpContext)
    {
        var user = httpContext.Items["User"] as StudentForResultDto;
        return user;
    }
}
