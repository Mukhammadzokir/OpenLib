using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.Publishers;

namespace OpenLibrary.Service.Interfaces;

public interface IPublisherService
{
    Task<bool> RemoveAsync(long id);
    Task<PublisherForResultDto> RetrieveByIdAsync(long id);
    Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto);
    Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto);
    Task<IEnumerable<PublisherForResultDto>> RetrieveAllAsync(PaginationParams @params);

}
