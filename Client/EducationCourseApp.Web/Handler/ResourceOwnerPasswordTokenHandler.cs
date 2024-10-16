﻿using System.Net.Http.Headers;
using System.Security.Principal;
using EducationCourseApp.Web.Exceptons;
using EducationCourseApp.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace EducationCourseApp.Web.Handler;

public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

    public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();
            if (tokenResponse is not null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
            
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizeException();
        }
        return response;

    }
}