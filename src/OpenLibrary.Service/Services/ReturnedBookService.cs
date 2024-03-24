using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.ReturnedBooks;

namespace OpenLibrary.Service.Services;

public class ReturnedBookService : IReturnedBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<ReturnedBook> _repository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IBorrowingRecordService _borrowingRecordService;
    private readonly IRepository<BorrowingRecord> _borrowingRecordRepository;

    public ReturnedBookService(
        IMapper mapper, 
        IRepository<Book> bookRepository,
        IRepository<ReturnedBook> repository,
        IRepository<Student> studentRepository,
        IBorrowingRecordService borrowingRecordService,
        IRepository<BorrowingRecord> borrowingRecordRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _bookRepository = bookRepository;
        _studentRepository = studentRepository;
        _borrowingRecordService = borrowingRecordService;
        _borrowingRecordRepository = borrowingRecordRepository;
    }

    public async Task<ReturnedBookForResultDto> AddAsync(ReturnedBookForCreationDto dto)
    {
        var mappedData = _mapper.Map<ReturnedBook>(dto);
        var createdData = await _repository.InsertAsync(mappedData);

        return _mapper.Map<ReturnedBookForResultDto>(createdData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReturnedBookForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var data = await _repository
            .SelectAll()
            .Include(a => a.Student)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReturnedBookForResultDto>>(data);
    }

    public async Task<ReturnedBookForResultDto> RetrieveByIdAsync(long id)
    {
        var data = await _repository
           .SelectAll()
           .Where(a => a.Id == id)
           .Include(a => a.Student)
           .AsNoTracking()
           .FirstOrDefaultAsync();
        if (data is null)
            throw new LibraryException(404, "ReturnedBook is not found");

        return _mapper.Map<ReturnedBookForResultDto>(data);
    }

    public async Task<bool> ReturnBookAsync(ReturnedBookForCreationDto dto)
    {
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var studentData = await _studentRepository
            .SelectAll()
            .Where(b => b.Id == dto.StudentId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");

        var returnedBook = await AddAsync(dto);

        var borrowingBookData = await _borrowingRecordRepository
            .SelectAll()
            .Where(br => br.BookId == dto.BookId && br.StudentId == dto.StudentId)
            .FirstOrDefaultAsync();

        if (borrowingBookData.ReturnDate <= DateTime.UtcNow)
        {

            var updatedReturnBook = new ReturnedBook()
            {
                BookId = returnedBook.BookId,
                StudentId = returnedBook.StudentId,
                ReturnedDate = DateTime.UtcNow,
                Fine = borrowingBookData.Fine,
            };

            await _repository.UpdateAsync(updatedReturnBook);
        }
        else
        {
            var updatedReturnBook = new ReturnedBook()
            {
                BookId = returnedBook.BookId,
                StudentId = returnedBook.StudentId,
                ReturnedDate = DateTime.UtcNow,
                Fine = 0,
            };
            await _repository.UpdateAsync(updatedReturnBook);

        }
        await _borrowingRecordService.RemoveAsync(borrowingBookData.Id);

        return true;
    }
}
