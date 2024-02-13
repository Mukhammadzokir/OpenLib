using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Api.Models;
using OpenLibrary.Service.Configurations;
using OpenLibrary.Service.DTOs.Books;
using OpenLibrary.Service.DTOs.ReturnedBooks;
using OpenLibrary.Service.Interfaces;

namespace OpenLibrary.Api.Controllers;

public class ReturnBooksController : BaseController
{
    private readonly IReturnedBookService _returnedBookService;

    public ReturnBooksController(IReturnedBookService returnedBookService)
    {
        _returnedBookService = returnedBookService;
    }

    [HttpPost]
    public async Task<IActionResult> ReturnBookAsync([FromBody] ReturnedBookForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._returnedBookService.ReturnBookAsync(dto)
        });

    [HttpGet]
    public async Task<IActionResult> GetAllReturnedBookAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._returnedBookService.RetrieveAllAsync(@params)
        });

}
