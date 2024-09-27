using EducationCourseApp.Discount.Services;
using EducationCourseApp.Shared.Controllers;
using EducationCourseApp.Shared.Dtos;
using EducationCourseApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Discount.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class DiscountController : CustomBaseController
{
   private readonly IDiscountService _discountService;
   private readonly ISharedIdentityService _sharedIdentity;

   public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentity)
   {
      _discountService = discountService;
      _sharedIdentity = sharedIdentity;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      return CreateActionResultInstance(await _discountService.GetAll());
   }

   [HttpGet]
   public async Task<IActionResult> GetById([FromQuery] int id)
   {
      return CreateActionResultInstance(await _discountService.GetById(id));
   }

   [HttpGet]
   public async Task<IActionResult> GetByCode(string code)
   {
      var userId = _sharedIdentity.GetUserId;
      return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, userId));
   }
   [HttpPost]
   public async Task<IActionResult> Save([FromBody] Models.Discount discount)
   {
      return CreateActionResultInstance(await _discountService.Save(discount));
   }

   [HttpDelete]
   public async Task<IActionResult> Delete(int id)
   {
      return CreateActionResultInstance(await _discountService.Delete(id));
   }

   [HttpPut]
   public async Task<IActionResult> Update(Models.Discount discount )
   {
      return CreateActionResultInstance(await _discountService.Update(discount));
   }
}