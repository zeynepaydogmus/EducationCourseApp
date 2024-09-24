using EducationCourseApp.Basket.Dtos;
using EducationCourseApp.Shared.Dtos;

namespace EducationCourseApp.Basket.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasket(string userId);
    Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
    Task<Response<bool>> Delete(string userId);
}