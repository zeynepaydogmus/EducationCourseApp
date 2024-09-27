using System.Data;
using Dapper;
using EducationCourseApp.Shared.Dtos;
using Npgsql;

namespace EducationCourseApp.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }
    
    public async Task<Response<List<Models.Discount>>> GetAll()
    {
        //queryAsync -> dapper
        var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount");
        return Response<List<Models.Discount>>.Success(discounts.ToList(),200);
    }

    public async Task<Response<Models.Discount>> GetById(int id)
    {
        var discount =
            (await _dbConnection.QueryAsync<Models.Discount>("Select * From discount where id=@Id", new { Id=id })).SingleOrDefault();
        if (discount is null)
        {
            return Response<Models.Discount>.Fail("Not Found.",404);
        }

        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> Save(Models.Discount discount)
    {
        var newDiscount = await _dbConnection.ExecuteAsync
                ("INSERT INTO discount (userid,rate,code) VALUES (@UserId,@Rate,@Code)",discount);
        if (newDiscount>0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Fail("An error occured while adding", 500);
    }

    public async Task<Response<NoContent>> Update(Models.Discount discount)
    {
        var status =
            await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id",
                discount);
        if (status>0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Fail("An error occured while updating", 500);
    }

    public async Task<Response<NoContent>> Delete(int id)
    {
        var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
        return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discount = (await _dbConnection.QueryAsync<Models.Discount>(
            "select * from discount where userid = @UserId and code=@Code", new { UserId = userId, Code = code })).SingleOrDefault();
        if (discount is null)
        {
            return Response<Models.Discount>.Fail("Not found.", 404);
        }

        return Response<Models.Discount>.Success(discount, 200);
    }
}