using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.BookAuthors;

namespace OpenLibrary.Service.Interfaces;

public interface IBookAuthorService
{
    Task<bool> RemoveAsync(long id);
    Task<BookAuthorForResultDto> RetrieveByIdAsync(long id);
    Task<BookAuthorForResultDto> AddAsync(BookAuthorForCreationDto dto);
    Task<BookAuthorForResultDto> ModifyAsync(long id, BookAuthorForUpdateDto dto);
    Task<IEnumerable<BookAuthorForResultDto>> RetrieveAllAsync(PaginationParams @params);

}
