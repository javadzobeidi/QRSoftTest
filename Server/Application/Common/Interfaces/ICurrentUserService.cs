using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface ICurrentUserService 
{
    string? UserName{ get; }
    int? UserId { get; }
    string? GivenName { get; }
    string Role { get; }
    public bool? IsAuthenticated { get; }
    IEnumerable<Claim> Claims { get; }

}
