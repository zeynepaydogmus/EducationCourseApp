using EducationCourseApp.Shared.Services;
using EducationCourseApp.Web.Models.Catalog;
using EducationCourseApp.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Web.Controllers;
[Authorize]
public class CoursesController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _identityService;

    public CoursesController(ISharedIdentityService identityService, ICatalogService catalogService)
    {
        _identityService = identityService;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _catalogService.GetAllCourseByUserIdAsync(_identityService.GetUserId);
        
        return View(courses ?? new List<CourseViewModel>());
    }
}