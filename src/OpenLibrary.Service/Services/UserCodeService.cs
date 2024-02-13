using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Domain.Entities;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.UserCodes;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;

namespace OpenLibrary.Service.Services;

public class UserCodeService : IUserCodeService
{
    private readonly IMapper _mapper;
    private readonly IRepository<UserCode> _repository;
    private readonly IRepository<Student> _userRepository;

    public UserCodeService(
        IMapper mapper,
        IRepository<UserCode> repository,
        IRepository<Student> userRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<UserCodeForResultDto> CreateAsync(UserCode model)
    {
        var user = await _userRepository
            .SelectAll()
            .Where(u => u.Id == model.UserId)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new LibraryException(404, "User is not found!");

        model.ExpireDate = DateTime.UtcNow.AddMinutes(3);

        var result = await _repository.InsertAsync(model);

        return _mapper.Map<UserCodeForResultDto>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var code = await _repository.SelectAll()
             .Where(c => c.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (code is null)
            throw new LibraryException(404, "Code is not found!");

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserCodeForResultDto>> GetAllAsync(PaginationParams @params)
    {
        var codes = await _repository
            .SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserCodeForResultDto>>(codes);
    }

    public async Task<UserCodeForResultDto> GetByIdAsync(long id)
    {
        var code = await _repository.SelectAll()
             .Where(c => c.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (code is null)
            throw new LibraryException(404, "Code is not found!");

        return _mapper.Map<UserCodeForResultDto>(code);
    }

}
