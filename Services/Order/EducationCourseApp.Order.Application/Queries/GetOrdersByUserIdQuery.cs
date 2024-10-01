using Azure;
using EducationCourseApp.Order.Application.Dtos;
using MediatR;

namespace EducationCourseApp.Order.Application.Queries;

public class GetOrdersByUserIdQuery : IRequest<Shared.Dtos.Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}