using System.Security.Claims;

using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    public string? Role => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    public string? GivenName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);

    public int? UserId => Int32.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));

    public IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext?.User?.Claims;

    public bool? IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
}
