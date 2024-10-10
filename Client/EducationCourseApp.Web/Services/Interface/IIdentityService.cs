using EducationCourseApp.Shared.Dtos;
using EducationCourseApp.Web.Models;
using IdentityModel;
using IdentityModel.Client;

namespace EducationCourseApp.Web.Services.Interface;

public interface IIdentityService
{
    Task<Response<bool>> SignIn(SignInInput signInInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}