﻿using EducationCourseApp.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Web.Controllers;
[Authorize]
public class UserController : Controller
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetUser());
    }
}