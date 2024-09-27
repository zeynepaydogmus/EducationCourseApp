using AutoMapper;
using EducationCourseApp.Order.Application.Dtos;
using EducationCourseApp.Order.Domain.OrderAggregate;

namespace EducationCourseApp.Order.Application.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }    
}