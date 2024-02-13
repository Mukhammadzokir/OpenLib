using AutoMapper;
using OpenLibrary.Domain.Roles;
using OpenLibrary.Service.Helpers;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.DTOs.Logins;
using OpenLibrary.Service.DTOs.Students;

namespace OpenLibrary.Service.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IStudentService _userService;
    private readonly IExistEmailService _existEmail;
    private readonly IRepository<Student> _repository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
                IMapper mapper,
                IStudentService userService,
                IExistEmailService existEmail,
                IRepository<Student> repository,
                IJwtTokenService jwtTokenService)
    {
        _mapper = mapper;
        _existEmail = existEmail;
        _repository = repository;
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<LoginForResultDto> AuthenticateAsync(LoginForCreationDto login)
    {
        var user = await _repository
              .SelectAll()
              .Where(u => u.Email == login.Email)
              .FirstOrDefaultAsync();

        var log = HelperPasswordHasher.PasswordHasher(login.Password);
        var log2 = HelperPasswordHasher.PasswordHasher("string");
        if (user is null || !HelperPasswordHasher.IsEqual(login.Password, user.Password))
            throw new LibraryException(404, "Email yoki parol xato!");

        if (!user.IsVerified)
            throw new LibraryException(403, "Iltimos avval pochtangizni tasdiqlang!");

        if (user.ExpireDate > DateTime.UtcNow)
        {
            (user.RefreshToken, user.ExpireDate) = await _jwtTokenService.GenerateRefreshTokenAsync();
            await _userService.ModifyAsync(user.Id, _mapper.Map<StudentForUpdateDto>(user));
        }

        var userView = _mapper.Map<StudentForResultDto>(user);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);
        return new LoginForResultDto
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = user.RefreshToken,
            User = userView
        };
    }

    public async Task<LoginForResultDto> CreateAsync(StudentForCreationDto model)
    {
        var user = await _repository
            .SelectAll()
            .Where(u => u.Email == model.Email)
            .FirstOrDefaultAsync();

        if (user is not null && !user.IsVerified)
            throw new LibraryException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochtangizni tasdiqlang va tizimga kiring!");

        if (user is not null)
            throw new LibraryException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochta va parol orqali tizimga kiring!");

        var mapped = _mapper.Map<Student>(model);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = HelperPasswordHasher.PasswordHasher(model.Password);
        mapped.Role = Role.Student;
        (mapped.RefreshToken, mapped.ExpireDate) = await _jwtTokenService.GenerateRefreshTokenAsync();

        var result = await _repository.InsertAsync(mapped);

        await _existEmail.ResendCodeAsync(model.Email);

        var userView = _mapper.Map<StudentForResultDto>(result);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);
        return new LoginForResultDto
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = mapped.RefreshToken,
            User = userView
        };
    }

    public async Task<bool> ChangePassword(string email, string password)
    {
        var user = await _repository.SelectAll()
             .Where(u => u.Email == email)
             .FirstOrDefaultAsync();

        if (user is null || HelperPasswordHasher.IsEqual(Constants.PASSWORD_SALT, user.Password))
            return false;

        user.Password = HelperPasswordHasher.PasswordHasher(password);
        return true;
    }

}
