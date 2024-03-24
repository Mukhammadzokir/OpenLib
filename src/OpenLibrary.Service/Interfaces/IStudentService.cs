using OpenLibrary.Service.DTOs.Students;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Interfaces;

public interface IStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<StudentForResultDto> RetrieveByIdAsync(long id);
    Task<decimal> RetrieveAllDebtsOfStudentAsync(long studentId);
    Task<StudentForResultDto> AddAsync(StudentForCreationDto dto);
    Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto);
    Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<StudentForResultDto>> GetAllDebtorStudentAsync(PaginationParams @params);
    Task<IEnumerable<StudentForResultDto>> RetrieveBarrowingBooksOfStudentAsync(long studentId, PaginationParams @params);
}
