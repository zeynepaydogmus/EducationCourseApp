using EducationCourseApp.Web.Models;
using EducationCourseApp.Web.Services.Interface;

namespace EducationCourseApp.Web.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserViewModel> GetUser()
    {
        return await _client.GetFromJsonAsync<UserViewModel>("/api/User/GetUser");
    }
    
}