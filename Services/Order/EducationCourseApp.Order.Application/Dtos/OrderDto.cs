﻿namespace EducationCourseApp.Order.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; private set; }
    public AddressDto Address { get; set; }
    public string  BuyerId { get; set; } 
    public List<OrderItemDto> OrderItems { get; set; }
}