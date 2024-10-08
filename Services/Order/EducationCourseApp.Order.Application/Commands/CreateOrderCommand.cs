﻿using EducationCourseApp.Order.Application.Dtos;
using EducationCourseApp.Shared.Dtos;
using MediatR;

namespace EducationCourseApp.Order.Application.Commands;

public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
    
}