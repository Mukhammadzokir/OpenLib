using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.ReturnedBooks;

namespace OpenLibrary.Api.Controllers;

public class ReturnBooksController : BaseController
{
    private readonly IReturnedBookService _returnedBookService;

    public ReturnBooksController(IReturnedBookService returnedBookService)
    {
        _returnedBookService = returnedBookService;
    }

    /// <summary>
    /// To return book that borrowing books. In addition if student return book later, 
    /// student are written a fine that set at the time of borrowing the book
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> ReturnBookAsync([FromBody] ReturnedBookForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._returnedBookService.ReturnBookAsync(dto)
        });

    /// <summary>
    /// To get all returned books
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> GetAllReturnedBookAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._returnedBookService.RetrieveAllAsync(@params)
        });

}
