
using EducationCourse.Order.Infrastructure;
using EducationCourseApp.Order.Application.Dtos;
using EducationCourseApp.Order.Application.Mapping;
using EducationCourseApp.Order.Application.Queries;
using EducationCourseApp.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationCourseApp.Order.Application.Handles;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Shared.Dtos.Response<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext orderDbContext)
    {
        _context = orderDbContext;
    }

    public async Task<Shared.Dtos.Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(x=>x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
        if (!orders.Any())
        {
            return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
        return Response<List<OrderDto>>.Success(ordersDto, 200);
    }
}