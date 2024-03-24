using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Api.Controllers;

public class GetAllDebtorStudentsController : BaseController
{
    private readonly IStudentService _studentService;

    public GetAllDebtorStudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// To get all debtor students
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._studentService.GetAllDebtorStudentAsync(@params)
        });
}
