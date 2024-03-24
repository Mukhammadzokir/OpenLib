using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.DTOs.Books;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Api.Controllers;

public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    /// <summary>
    /// To add books
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.AddAsync(dto)
        });

    /// <summary>
    /// To get all books 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To get books by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To get books by ISBN code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("isbn/{isbn}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "isbn")] string code)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.RetrieveByISBNAsync(code)
        });

    /// <summary>
    /// To update book by id 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BookForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete books by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookService.RemoveAsync(id)
        });
}
