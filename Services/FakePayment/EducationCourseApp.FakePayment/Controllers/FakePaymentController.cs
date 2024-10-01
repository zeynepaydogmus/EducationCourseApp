using EducationCourseApp.Shared.Controllers;
using EducationCourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.FakePayment.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class FakePaymentController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> ReceivePayment()
    {
        return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
    } 
}