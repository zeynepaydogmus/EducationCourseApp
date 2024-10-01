using EducationCourse.Order.Infrastructure;
using EducationCourseApp.Order.Application.Commands;
using EducationCourseApp.Order.Application.Dtos;
using EducationCourseApp.Shared.Dtos;
using MediatR;

namespace EducationCourseApp.Order.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAdress = new Domain.OrderAggregate.Address(
            request.Address.Province,
            request.Address.District,
            request.Address.Street,
            request.Address.ZipCode,
            request.Address.Line);
        Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAdress);

        request.OrderItems.ForEach(x => { newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.ImageUrl); });
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        return Response<CreatedOrderDto>.Success(new CreatedOrderDto
        {
            OrderId =
                newOrder.Id
        }, 200);
    }
}