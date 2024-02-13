using OpenLibrary.Domain.Entities;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.UserCodes;

namespace OpenLibrary.Service.Interfaces;

public interface IUserCodeService
{
    Task<bool> DeleteAsync(long id);
    Task<UserCodeForResultDto> GetByIdAsync(long id);
    Task<UserCodeForResultDto> CreateAsync(UserCode model);
    Task<IEnumerable<UserCodeForResultDto>> GetAllAsync(PaginationParams @params);
}
