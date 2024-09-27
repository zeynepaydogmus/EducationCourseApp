﻿namespace EducationCourseApp.Order.Application.Dtos;

public class OrderItemDto
{
    
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ImageUrl { get; private set; }
    public decimal Price { get; private set; }
}