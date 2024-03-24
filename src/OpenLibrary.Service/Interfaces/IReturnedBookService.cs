using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.ReturnedBooks;

namespace OpenLibrary.Service.Interfaces;

public interface IReturnedBookService
{
    Task<bool> RemoveAsync(long id);
    Task<ReturnedBookForResultDto> RetrieveByIdAsync(long id);
    Task<bool> ReturnBookAsync(ReturnedBookForCreationDto dto);
    Task<ReturnedBookForResultDto> AddAsync(ReturnedBookForCreationDto dto);

    Task<IEnumerable<ReturnedBookForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
