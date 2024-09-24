using System.Text.Json;
using EducationCourseApp.Basket.Dtos;
using EducationCourseApp.Shared.Dtos;

namespace EducationCourseApp.Basket.Services;

public class BasketService: IBasketService
{
 
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<BasketDto>> GetBasket(string userId)
    {
        var isExistBasket = await _redisService.GetDb().StringGetAsync(userId);
        if (String.IsNullOrEmpty(isExistBasket))
        {
            return Response<BasketDto>.Fail("Not Found.", 404);
        }
        return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(isExistBasket), 200);
    }

    public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket couldn't update or save.",500);
    }

    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found.",404);
    }
}