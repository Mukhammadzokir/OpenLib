using System.Net;
using System.Net.Mail;
using OpenLibrary.Domain.Roles;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using OpenLibrary.Domain.Entities.Messages;

namespace OpenLibrary.Service.Services;

public class ExistEmailService : IExistEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<Student> _repository;
    private readonly IUserCodeService _userCodeService;
    private readonly IRepository<UserCode> _codeRepository;

    public ExistEmailService(
        IConfiguration configuration,
        IRepository<Student> repository,
        IUserCodeService userCodeService,
        IRepository<UserCode> codeRepository)
    {
        _repository = repository;
        _codeRepository = codeRepository;
        _userCodeService = userCodeService;
        _configuration = configuration.GetSection("Email");
    }

    public async Task<ExistEmailEnum> EmailExistance(string email)
    {
        var user = await _repository
            .SelectAll()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
            return ExistEmailEnum.EmailNotFound;

        if (user.IsVerified)
            return ExistEmailEnum.EmailFound;

        var resend = await ResendCodeAsync(email);

        if (!resend)
            throw new LibraryException(403, "Birozdan keyinroq qayta urinib ko'ring!");

        return ExistEmailEnum.EmailNotChecked;
    }

    public async Task<bool> VerifyCodeAsync(string email, long code)
    {
        var userCodeAny = await _codeRepository
            .SelectAll()
            .Include(u => u.User)
            .AnyAsync(c => c.User.Email == email && c.ExpireDate > DateTime.UtcNow && c.Code == code);

        if (userCodeAny)
        {
            var user = await _repository
                .SelectAll()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                return false;

            if (!user.IsVerified)
            {
                user.IsVerified = true;
                await _repository.UpdateAsync(user);
            }
            return true;
        }

        return false;
    }

    public async Task<bool> ResendCodeAsync(string email)
    {
        var userCodeAny = await _codeRepository.SelectAll()
            .AnyAsync(c => c.User.Email == email && c.ExpireDate > DateTime.UtcNow);

        if (userCodeAny)
            return false;

        var user = await _repository
            .SelectAll()
            .Where(u => u.Email == email)
            .Select(u => new { u.Id })
            .FirstOrDefaultAsync();

        if (user is null)
            return false;

        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Bu kodni boshqalarga bermang!",
            To = email,
            Body = $"Sizning tasdiqlash kodingiz: {randomNumber}"
        };


        var userCode = new UserCode()
        {
            Code = randomNumber,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddMinutes(3)
        };

        _ = await _userCodeService.CreateAsync(userCode);
        await this.SendMessage(message);

        return true;
    }

    public Task SendMessage(Message message)
    {
        var _smtpModel = new
        {
            Host = _configuration["Host"],
            Email = (string)_configuration["EmailAddress"],
            Port = 587,
            AppPassword = _configuration["Password"]
        };

        using (MailMessage mm = new MailMessage(_smtpModel.Email, message.To))
        {
            mm.Subject = message.Subject;
            mm.Body = message.Body;
            mm.IsBodyHtml = false;
            using (System.Net.Mail.SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = _smtpModel.Host;
                smtp.Port = _smtpModel.Port;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(_smtpModel.Email, _smtpModel.AppPassword);
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
            }
        }

        return Task.CompletedTask;
    }
}
