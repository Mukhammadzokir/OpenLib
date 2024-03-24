using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.BorrowingRecords;

namespace OpenLibrary.Api.Controllers;

public class BorrowingRecordsController : BaseController
{
    private readonly IBorrowingRecordService _borrowingRecordService;

    public BorrowingRecordsController(IBorrowingRecordService borrowingRecordService)
    {
        _borrowingRecordService = borrowingRecordService;
    }

    /// <summary>
    /// To borrow a book by studentId and bookId and mark the returned time. 
    /// In addition, when book are borrowed, automatically set borrowing date
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BorrowingRecordForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.AddAsync(dto)
        });

    /// <summary>
    /// To get all borrowed books
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To get any borrowed date by borrowing id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update information of borrowed books
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BorrowingRecordForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete borrowed books by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RemoveAsync(id)
        });
}

