using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Api.Controllers;

public class GetBorrowingBooksOfStudentAsync : BaseController
{
    private readonly IStudentService _studentService;

    public GetBorrowingBooksOfStudentAsync(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// To get all borrowed book of any student by student id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "studentId")] long id, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._studentService.RetrieveBarrowingBooksOfStudentAsync(id, @params)
        });
}
