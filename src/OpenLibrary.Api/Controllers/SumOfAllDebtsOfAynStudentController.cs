using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;
using OpenLibrary.Service.Configurations;

namespace OpenLibrary.Api.Controllers;

public class SumOfAllDebtsOfAynStudentController : BaseController
{
    private readonly IStudentService _studentService;

    public SumOfAllDebtsOfAynStudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetAllAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._studentService.RetrieveAllDebtsOfStudentAsync(studentId)
        });
}
