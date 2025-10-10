using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.UseCases.Services;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
  private readonly AuthenticationStateProvider _authenticationStateProvider;
  private readonly ILogger<AuthenticationDelegatingHandler> _logger;

  public AuthenticationDelegatingHandler(
      AuthenticationStateProvider authenticationStateProvider,
      ILogger<AuthenticationDelegatingHandler> logger)
  {
    _authenticationStateProvider = authenticationStateProvider;
    _logger = logger;
  }

  protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
  {
    try
    {
      var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

      if (authState.User.Identity?.IsAuthenticated == true)
      {
        await ForwardUserClaimsAsync(request, authState.User);
      }
    }
    catch (Exception ex)
    {
      _logger.LogWarning(ex, "Failed to forward authentication information");
    }

    return await base.SendAsync(request, cancellationToken);
  }

  private async Task ForwardUserClaimsAsync(HttpRequestMessage request, ClaimsPrincipal user)
  {
    // Option 1: Forward specific claims as headers
    ForwardClaimsAsHeaders(request, user);

    // Option 2: Forward user identity as custom header
    ForwardUserIdentity(request, user);

    // Option 3: If you have access tokens, forward them as Bearer tokens
    await ForwardAccessTokenAsync(request, user);
  }

  private void ForwardClaimsAsHeaders(HttpRequestMessage request, ClaimsPrincipal user)
  {
    // Forward user ID
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!string.IsNullOrEmpty(userId))
    {
      request.Headers.Add("X-User-Id", userId);
    }

    // Forward user email
    var email = user.FindFirst(ClaimTypes.Email)?.Value ?? user.FindFirst(ClaimTypes.Name)?.Value;
    if (!string.IsNullOrEmpty(email))
    {
      request.Headers.Add("X-User-Email", email);
    }

    // Forward user roles
    var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
    if (roles.Any())
    {
      request.Headers.Add("X-User-Roles", string.Join(",", roles));
    }

    // Forward custom claims (if any)
    var customClaims = user.Claims
        .Where(c => !IsStandardClaim(c.Type))
        .Select(c => $"{c.Type}:{c.Value}")
        .ToList();

    if (customClaims.Any())
    {
      request.Headers.Add("X-User-Claims", string.Join(";", customClaims));
    }
  }

  private void ForwardUserIdentity(HttpRequestMessage request, ClaimsPrincipal user)
  {
    var userName = user.Identity?.Name;
    if (!string.IsNullOrEmpty(userName))
    {
      request.Headers.Add("X-Forwarded-User", userName);
    }
  }

  private async Task ForwardAccessTokenAsync(HttpRequestMessage request, ClaimsPrincipal user)
  {
    // If you're using JWT tokens or external authentication providers,
    // you can retrieve and forward the access token

    // Example for JWT tokens stored in claims:
    var accessToken = user.FindFirst("access_token")?.Value;
    if (!string.IsNullOrEmpty(accessToken))
    {
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      await Task.CompletedTask;
    }

    // Alternative: If using IHttpContextAccessor to get tokens from HttpContext
    // This would require additional setup and dependency injection
    await Task.CompletedTask;
  }

  private static bool IsStandardClaim(string claimType)
  {
    return claimType switch
    {
      ClaimTypes.NameIdentifier => true,
      ClaimTypes.Name => true,
      ClaimTypes.Email => true,
      ClaimTypes.Role => true,
      ClaimTypes.GivenName => true,
      ClaimTypes.Surname => true,
      "aud" => true,
      "iss" => true,
      "exp" => true,
      "iat" => true,
      _ => false
    };
  }
}
