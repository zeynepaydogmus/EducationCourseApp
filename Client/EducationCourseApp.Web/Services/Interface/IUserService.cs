using EducationCourseApp.Web.Models;

namespace EducationCourseApp.Web.Services.Interface;

public interface IUserService
{
    Task<UserViewModel> GetUser();
    
}