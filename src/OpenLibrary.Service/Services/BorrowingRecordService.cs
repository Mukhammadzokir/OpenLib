using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.BorrowingRecords;

namespace OpenLibrary.Service.Services;

public class BorrowingRecordService : IBorrowingRecordService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<BorrowingRecord> _borrowingRecordRepository;

    public BorrowingRecordService(
        IMapper mapper, 
        IRepository<Book> bookRepository, 
        IRepository<Student> studentRepository,
        IRepository<BorrowingRecord> borrowingRecordRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _studentRepository = studentRepository;
        _borrowingRecordRepository = borrowingRecordRepository;
    }

    public async Task<BorrowingRecordForResultDto> AddAsync(BorrowingRecordForCreationDto dto)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == dto.StudentId)
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");
        
        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var borrowingRecordData = await _borrowingRecordRepository
            .SelectAll()
            .Where(br => br.StudentId == dto.StudentId && br.BookId == dto.BookId)
            .FirstOrDefaultAsync();
        if (borrowingRecordData is not null)
            throw new LibraryException(409, "BorrowingRecord is already exist");

        var mappedData = _mapper.Map<BorrowingRecord>(dto);
        var createdData = await _borrowingRecordRepository.InsertAsync(mappedData);

        return _mapper.Map<BorrowingRecordForResultDto>(createdData);
    }

    public async Task<BorrowingRecordForResultDto> ModifyAsync(long id, BorrowingRecordForUpdateDto dto)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == dto.StudentId)
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");

        var bookData = await _bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .FirstOrDefaultAsync();
        if (bookData is null)
            throw new LibraryException(404, "Book is not found");

        var borrowingRecordData = await _borrowingRecordRepository
            .SelectAll()
            .Where(br => br.Id == id)
            .FirstOrDefaultAsync();
        if (borrowingRecordData is null)
            throw new LibraryException(409, "BorrowingRecord is already exist");

        var mappedData = _mapper.Map(dto, borrowingRecordData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _borrowingRecordRepository.UpdateAsync(mappedData);

        return _mapper.Map<BorrowingRecordForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var borrowingRecordData = await _borrowingRecordRepository
            .SelectAll()
            .Where(br => br.Id == id)
            .FirstOrDefaultAsync();
        if (borrowingRecordData is null)
            throw new LibraryException(404, "BorrowingRecord is not found");

        return await _borrowingRecordRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BorrowingRecordForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var borrowingRecordData = await _borrowingRecordRepository
           .SelectAll()
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<BorrowingRecordForResultDto>>(borrowingRecordData);
    }

    public async Task<BorrowingRecordForResultDto> RetrieveByIdAsync(long id)
    {
        var borrowingRecordData = await _borrowingRecordRepository
            .SelectAll()
            .Where(a => a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (borrowingRecordData is null)
            throw new LibraryException(404, "BorrowingRecord is not found");

        return _mapper.Map<BorrowingRecordForResultDto>(borrowingRecordData);
    }
}
