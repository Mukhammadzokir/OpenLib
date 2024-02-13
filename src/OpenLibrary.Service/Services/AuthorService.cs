using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.DTOs.Authors;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Author> _repository;

    public AuthorService(IMapper mapper, IRepository<Author> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<AuthorForResultDto> AddAsync(AuthorForCreationDto dto)
    {
        var authorData = await _repository
            .SelectAll()
            .Where(a => a.Name.ToLower() == dto.Name.ToLower())
            .FirstOrDefaultAsync();
        if (authorData is not null)
            throw new LibraryException(409, "Author is already exist");

        var mappedData = _mapper.Map<Author>(dto);
        var createdData = await _repository.InsertAsync(mappedData);

        return _mapper.Map<AuthorForResultDto>(createdData);
    }

    public async Task<AuthorForResultDto> ModifyAsync(long id, AuthorForUpdateDto dto)
    {
        var authorData = await _repository
            .SelectAll()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
        if (authorData is null)
            throw new LibraryException(404, "Author is not found");

        var mappedData = _mapper.Map(dto, authorData);
        mappedData.UpdatedAt = DateTime.UtcNow;
        
        await _repository.UpdateAsync(mappedData);

        return _mapper.Map<AuthorForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var authorData = await _repository
            .SelectAll()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
        if(authorData is null)
            throw new LibraryException(404, "Author is not found");

        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AuthorForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var authorData = await _repository
            .SelectAll()
            .Include(a => a.Books)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AuthorForResultDto>>(authorData);
    }

    public async Task<AuthorForResultDto> RetrieveByIdAsync(long id)
    {
        var authorData = await _repository
            .SelectAll()
            .Where(a => a.Id == id)
            .Include(a => a.Books)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if(authorData is null)
            throw new LibraryException(404,"Author is not found");

        return _mapper.Map<AuthorForResultDto>(authorData);
    }
}
