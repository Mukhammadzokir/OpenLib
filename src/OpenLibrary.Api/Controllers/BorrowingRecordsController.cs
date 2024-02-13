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

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BorrowingRecordForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.AddAsync(dto)
        });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RetrieveAllAsync(@params)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RetrieveByIdAsync(id)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BorrowingRecordForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.ModifyAsync(id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._borrowingRecordService.RemoveAsync(id)
        });
}

