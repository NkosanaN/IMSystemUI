using Microsoft.AspNetCore.Authentication;

namespace IMSystemUI.UI.Middleware;

public class TokenBearerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenBearerMiddleware(RequestDelegate next, IConfiguration configuration, 
        IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var token = await GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", $"Bearer {token}");
        }

        await _next(context);
    }

    private async Task<string> GetTokenAsync()
    {
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        return token;
    }
}
