using EducationCourseApp.Basket.Dtos;
using EducationCourseApp.Basket.Services;
using EducationCourseApp.Shared.Controllers;
using EducationCourseApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Basket.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class BasketController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _identityService;

    public BasketController(IBasketService basketService, ISharedIdentityService ıdentityService)
    {
        _basketService = basketService;
        _identityService = ıdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var claims = User.Claims;
        return CreateActionResultInstance(await _basketService.GetBasket(_identityService.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdate([FromBody] BasketDto basket)
    {
        var response= await _basketService.SaveOrUpdate(basket);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return CreateActionResultInstance(await _basketService.Delete(_identityService.GetUserId));
    }
}