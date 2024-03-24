using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.Publishers;

namespace OpenLibrary.Service.Services;

public class PublisherService : IPublisherService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Publisher> _publisherRepository;

    public PublisherService(IMapper mapper, IRepository<Publisher> publisherRepository)
    {
        _mapper = mapper;
        _publisherRepository = publisherRepository;
    }

    public async Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto)
    {
        var publisherData = await _publisherRepository
            .SelectAll()
            .Where(p => p.Name.ToLower() == dto.Name.ToLower())
            .FirstOrDefaultAsync();
        if (publisherData is not null)
            throw new LibraryException(409, "Publisher is already exist");

        var mappedData = _mapper.Map<Publisher>(dto);
        var createdData = await _publisherRepository.InsertAsync(mappedData);

        return _mapper.Map<PublisherForResultDto>(createdData);
    }

    public async Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto)
    {
        var publisherData = await _publisherRepository
            .SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (publisherData is null)
            throw new LibraryException(404, "Publisher is not found");

        var mappedData = _mapper.Map(dto, publisherData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _publisherRepository.UpdateAsync(mappedData);

        return _mapper.Map<PublisherForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var publisherData = await _publisherRepository
           .SelectAll()
           .Where(p => p.Id == id)
           .FirstOrDefaultAsync();
        if (publisherData is null)
            throw new LibraryException(404, "Publisher is not found");

        return await _publisherRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PublisherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var publisherData = await _publisherRepository
          .SelectAll()
          .Include(p => p.Books)
          .AsNoTracking()
          .ToPagedList(@params)
          .ToListAsync();

        return _mapper.Map<IEnumerable<PublisherForResultDto>>(publisherData);
    }

    public async Task<PublisherForResultDto> RetrieveByIdAsync(long id)
    {
        var publisherData = await _publisherRepository
          .SelectAll()
          .Where(p => p.Id == id)
          .Include(p => p.Books)
          .AsNoTracking()
          .FirstOrDefaultAsync();
        if (publisherData is null)
            throw new LibraryException(404, "Publisher is not found");

        return _mapper.Map<PublisherForResultDto>(publisherData);
    }
}
