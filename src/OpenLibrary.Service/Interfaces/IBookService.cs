using OpenLibrary.Service.DTOs.Books;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Interfaces;

public interface IBookService
{
    Task<bool> RemoveAsync(long id);
    Task<BookForResultDto> RetrieveByIdAsync(long id);
    Task<BookForResultDto> RetrieveByISBNAsync(string code);
    Task<BookForResultDto> AddAsync(BookForCreationDto dto);
    Task<BookForResultDto> ModifyAsync(long id,BookForUpdateDto dto);
    Task<IEnumerable<BookForResultDto>> RetrieveAllAsync(PaginationParams @params);

}
