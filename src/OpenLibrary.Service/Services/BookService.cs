using AutoMapper;
using OpenLibrary.Domain.Entities;
using OpenLibrary.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.DTOs.Books;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Services;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Publisher> _publisherRepository;

    public BookService(
        IMapper mapper, 
        IRepository<Book> bookRepository,
        IRepository<Publisher> publisherRepository
        )
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<BookForResultDto> AddAsync(BookForCreationDto dto)
    {
        var publisherData = await _publisherRepository
            .SelectAll()
            .Where(p => p.Id == dto.PublisherId)
            .FirstOrDefaultAsync();
        if(publisherData is null)
            throw new LibraryException(404,"Publisher is not found");

        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Title.ToLower() == dto.Title.ToLower())
            .FirstOrDefaultAsync();
        if (bookData is not null)
            throw new LibraryException(409, "Book is already exist");

        var mappedData = _mapper.Map<Book>(dto);
        mappedData.ISBN = Generator.GenerateISBN13();
        var createdData = await _bookRepository.InsertAsync(mappedData);

        return _mapper.Map<BookForResultDto>(createdData);
    }

    public async Task<BookForResultDto> ModifyAsync(long id, BookForUpdateDto dto)
    {
        var publisherData = await _publisherRepository
            .SelectAll()
            .Where(p => p.Id == dto.PublisherId)
            .FirstOrDefaultAsync();
        if (publisherData is null)
            throw new LibraryException(404, "Publisher is not found");

        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var mappedData = _mapper.Map(dto, bookData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _bookRepository.UpdateAsync(mappedData);

        return _mapper.Map<BookForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        return await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var bookData = await _bookRepository
           .SelectAll()
           .Include(b => b.Publisher)
           .Include(b => b.Authors)
           .Include(b => b.BorrowingRecords)
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<BookForResultDto>>(bookData);
    }

    public async Task<BookForResultDto> RetrieveByIdAsync(long id)
    {
        var bookData = await _bookRepository
           .SelectAll()
           .Where(b => b.Id == id)
           .Include(b => b.Publisher)
           .Include(b => b.Authors)
           .Include(b => b.BorrowingRecords)
           .AsNoTracking()
           .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        return _mapper.Map<BookForResultDto>(bookData);
    }

    public async Task<BookForResultDto> RetrieveByISBNAsync(string code)
    {
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.ISBN == code)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "ISBN is wrong or Book is not found");

        return _mapper.Map<BookForResultDto>(bookData);
    }
}
