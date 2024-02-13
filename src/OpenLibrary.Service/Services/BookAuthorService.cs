using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.BookAuthors;

namespace OpenLibrary.Service.Services;

public class BookAuthorService : IBookAuthorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<BookAuthor> _bookAuthorRepository;

    public BookAuthorService(
        IMapper mapper,
        IRepository<Book> bookRepository,
        IRepository<Author> authorRepository,   
        IRepository<BookAuthor> bookAuthorRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _bookAuthorRepository = bookAuthorRepository;
    }

    public async Task<BookAuthorForResultDto> AddAsync(BookAuthorForCreationDto dto)
    {
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var authorData = await _authorRepository
            .SelectAll()
            .Where(a => a.Id == dto.AuthorId)
            .FirstOrDefaultAsync();
        if (authorData is null)
            throw new LibraryException(404, "Author is not found");

        var bookAuthorData = await _bookAuthorRepository
            .SelectAll()
            .Where(ba => ba.BookId == dto.BookId && ba.AuthorId == dto.AuthorId)
            .FirstOrDefaultAsync();
        if (bookAuthorData is not null)
            throw new LibraryException(409, "BookAuthor is already exist");

        var mappedData = _mapper.Map<BookAuthor>(dto);
        var createdData = await _bookAuthorRepository.InsertAsync(mappedData);

        return _mapper.Map<BookAuthorForResultDto>(createdData);

    }

    public async Task<BookAuthorForResultDto> ModifyAsync(long id, BookAuthorForUpdateDto dto)
    {
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var authorData = await _authorRepository
            .SelectAll()
            .Where(a => a.Id == dto.AuthorId)
            .FirstOrDefaultAsync();
        if (authorData is null)
            throw new LibraryException(404, "Author is not found");

        var bookAuthorData = await _bookAuthorRepository
            .SelectAll()
            .Where(ba => ba.Id == id)
            .FirstOrDefaultAsync();
        if (bookAuthorData is null)
            throw new LibraryException(404, "BookAuthor is not found");

        var mappedData = _mapper.Map(dto, bookAuthorData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _bookAuthorRepository.UpdateAsync(mappedData);

        return _mapper.Map<BookAuthorForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var bookAuthorData = await _bookAuthorRepository
          .SelectAll()
          .Where(ba => ba.Id == id)
          .FirstOrDefaultAsync();
        if (bookAuthorData is null)
            throw new LibraryException(404, "BookAuthor is not found");

        return await _bookAuthorRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookAuthorForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {

        var bookAuthorData = await _bookAuthorRepository
            .SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookAuthorForResultDto>>(bookAuthorData);
    }

    public async Task<BookAuthorForResultDto> RetrieveByIdAsync(long id)
    {
        var bookAuthorData = await _bookAuthorRepository
            .SelectAll()
            .Where(a => a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (bookAuthorData is null)
            throw new LibraryException(404, "BookAuthor is not found");

        return _mapper.Map<BookAuthorForResultDto>(bookAuthorData);
    }
}
