using OpenLibrary.Api.Models;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Service.Interfaces;

namespace OpenLibrary.Api.Controllers;

public class SumOfAllDebtsOfAynStudentController : BaseController
{
    private readonly IStudentService _studentService;

    public SumOfAllDebtsOfAynStudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// To get all debts of ant student by studentId 
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetAllAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._studentService.RetrieveAllDebtsOfStudentAsync(studentId)
        });
}
