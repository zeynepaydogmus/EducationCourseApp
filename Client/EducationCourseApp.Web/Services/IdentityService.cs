using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using EducationCourseApp.Shared.Dtos;
using EducationCourseApp.Web.Models;
using EducationCourseApp.Web.Services.Interface;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace EducationCourseApp.Web.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClientSettings _clientSettings;
    private readonly ServiceApiSettings _serviceApiSettings;

    public IdentityService(HttpClient client, IHttpContextAccessor httpContextAccessor,
        IOptions<ClientSettings> clientSettings,
        IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _clientSettings = clientSettings.Value;
        _serviceApiSettings = serviceApiSettings.Value;
        _client = client;
    }

    public async Task<Response<bool>> SignIn(SignInInput signInInput)
    {
        var discovery = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }

        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.WebClientForUser.ClientId,
            ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
            UserName = signInInput.Email,
            Password = signInInput.Password,
            Address = discovery.TokenEndpoint
        };

        var token = await _client.RequestPasswordTokenAsync(passwordTokenRequest);
        if (token.IsError)
        {
            var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();
            var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Response<bool>.Fail(errorDto.Errors, 404);
        }

        var userInfoRequest = new UserInfoRequest()
        {
            Token = token.AccessToken,
            Address = discovery.UserInfoEndpoint
        };

        var userInfo = await _client.GetUserInfoAsync(userInfoRequest);
        if (userInfo.IsError)
        {
            throw userInfo.Exception;
        }

        //cookie oluşurken hangi kimlikten oluşacak;
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims,
            CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        //access,refresh token tutalım
        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.StoreTokens(new List<AuthenticationToken>()
        {
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },

            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },

            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
            }
        });

        authenticationProperties.IsPersistent = signInInput.IsRemember;
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authenticationProperties);
        return Response<bool>.Success(200);
    }

    public async Task<TokenResponse> GetAccessTokenByRefreshToken()
    {
        var discovery = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }

        var refreshToken =
            await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
        RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest
        {
            ClientId = _clientSettings.WebClientForUser.ClientId,
            ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
            RefreshToken = refreshToken,
            Address = discovery.TokenEndpoint
        };

        var token = await _client.RequestRefreshTokenAsync(refreshTokenRequest);
        if (token.IsError)
        {
            return null;
        }

        var authenticationTokens = new List<AuthenticationToken>()
        {
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
            }
        };

        var authenticationResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();
        var properties = authenticationResult.Properties;
        properties.StoreTokens(authenticationTokens);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            authenticationResult.Principal, properties);
        return token;
    }

    public async Task RevokeRefreshToken()
    {
        var discovery = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }

        var refreshToken =
            await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        TokenRevocationRequest tokenRevocationRequest = new TokenRevocationRequest
        {
            ClientId = _clientSettings.WebClientForUser.ClientId,
            ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
            Address = discovery.RevocationEndpoint,
            Token = refreshToken,
            TokenTypeHint = "refresh_token"
        };

        await _client.RevokeTokenAsync(tokenRevocationRequest);
    }
}