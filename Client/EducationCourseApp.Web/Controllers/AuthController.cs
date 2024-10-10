using EducationCourseApp.Web.Models;
using EducationCourseApp.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Web.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _identityService.SignIn(signInInput);
        if (!response.IsSuccessful)
        {
            response.Errors.ForEach(x =>
            {
                ModelState.AddModelError(string.Empty, x);
            });
        }
        
        return RedirectToAction(nameof(Index), "Home");
        
    }
}