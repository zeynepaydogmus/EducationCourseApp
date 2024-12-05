namespace EducationCourseApp.Web.Services.Interface;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}