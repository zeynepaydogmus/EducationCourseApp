using EducationCourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Shared.Controllers;

public class CustomBaseController : ControllerBase
{
    
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}