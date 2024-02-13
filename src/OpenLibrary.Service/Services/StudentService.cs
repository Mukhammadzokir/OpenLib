using AutoMapper;
using OpenLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Data.IRepositories;
using OpenLibrary.Service.Exceptions;
using OpenLibrary.Service.Extensions;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.DTOs.Students;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Services;

public class StudentService : IStudentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Student> _studentRepository;

    public StudentService(IMapper mapper, IRepository<Student> studentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    public async Task<StudentForResultDto> AddAsync(StudentForCreationDto dto)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Email.ToLower() == dto.Email.ToLower())
            .FirstOrDefaultAsync();
        if (studentData is not null)
            throw new LibraryException(409, "Student is already exist");

        var mappedData = _mapper.Map<Student>(dto);
        var createdData = await _studentRepository.InsertAsync(mappedData);

        return _mapper.Map<StudentForResultDto>(createdData);
    }

    public async Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto)
    {
        var studentData = await _studentRepository
           .SelectAll()
           .Where(s => s.Id == id)
           .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");

        var mappedData = _mapper.Map(dto, studentData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _studentRepository.UpdateAsync(mappedData);

        return _mapper.Map<StudentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");

        return await _studentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Include(s => s.ReturnedBooks)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }

    public async Task<StudentForResultDto> RetrieveByIdAsync(long id)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == id)
            .Include(a => a.BorrowingRecords)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new LibraryException(404, "Student is not found");

        return _mapper.Map<StudentForResultDto>(studentData);
    }

    public async Task<IEnumerable<StudentForResultDto>> RetrieveBarrowingBooksOfStudentAsync(long studentId, PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == studentId)
            .Include(s => s.BorrowingRecords)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }

    public async Task<decimal> RetrieveAllDebtsOfStudentAsync(long studentId)
    {
        var listOfDebtsOfStudent = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == studentId)
            .Include(s => s.ReturnedBooks)
            .FirstOrDefaultAsync();

        var returnBooks = listOfDebtsOfStudent.ReturnedBooks.Where(r => r.Fine > 0);

        return returnBooks.Sum(r => r.Fine);
    }

    public async Task<IEnumerable<StudentForResultDto>> GetAllDebtorStudentAsync(PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.ReturnedBooks.Any(r => r.Fine > 0))
            //.Include(s => s.ReturnedBooks)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }
}
