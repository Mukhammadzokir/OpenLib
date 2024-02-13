using OpenLibrary.Domain.Roles;
using OpenLibrary.Domain.Entities.Messages;

namespace OpenLibrary.Service.Interfaces;

public interface IExistEmailService
{
    Task<ExistEmailEnum> EmailExistance(string email);

    Task SendMessage(Message message);

    Task<bool> VerifyCodeAsync(string email, long code);

    Task<bool> ResendCodeAsync(string email);
}
