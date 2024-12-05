using System.Net;
using System.Net.Http.Headers;
using EducationCourseApp.Web.Exceptons;
using EducationCourseApp.Web.Services;
using EducationCourseApp.Web.Services.Interface;

namespace EducationCourseApp.Web.Handler;

public class ClientCredentialTokenHandlerİ:DelegatingHandler
{
    private readonly IClientCredentialTokenService _clientCredential;

    public ClientCredentialTokenHandlerİ(IClientCredentialTokenService clientCredential)
    {
        _clientCredential = clientCredential;
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bareer",await _clientCredential.GetToken());
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizeException();
        }

        return response;
        
    }
}