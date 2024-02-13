using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.BorrowingRecords;

namespace OpenLibrary.Service.Interfaces;

public interface IBorrowingRecordService
{
    Task<bool> RemoveAsync(long id);
    Task<BorrowingRecordForResultDto> RetrieveByIdAsync(long id);
    Task<BorrowingRecordForResultDto> AddAsync(BorrowingRecordForCreationDto dto);
    Task<BorrowingRecordForResultDto> ModifyAsync(long id, BorrowingRecordForUpdateDto dto);
    Task<IEnumerable<BorrowingRecordForResultDto>> RetrieveAllAsync(PaginationParams @params);

}
