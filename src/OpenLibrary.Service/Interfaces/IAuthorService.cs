using OpenLibrary.Service.DTOs.Authors;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Service.Interfaces;

public interface IAuthorService
{
    Task<bool> RemoveAsync(long id);
    Task<AuthorForResultDto> RetrieveByIdAsync(long id);
    Task<AuthorForResultDto> AddAsync(AuthorForCreationDto dto);
    Task<AuthorForResultDto> ModifyAsync(long id, AuthorForUpdateDto dto);
    Task<IEnumerable<AuthorForResultDto>> RetrieveAllAsync(PaginationParams @params);

}
