using EducationCourseApp.Order.Application.Commands;
using EducationCourseApp.Order.Application.Queries;
using EducationCourseApp.Shared.Controllers;
using EducationCourseApp.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Order.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _identityService;

    public OrderController(IMediator mediator, ISharedIdentityService identityService)
    {
        _mediator = mediator;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery{UserId = _identityService.GetUserId});
        return CreateActionResultInstance(response);
        
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(CreateOrderCommand command)
    {
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }
}