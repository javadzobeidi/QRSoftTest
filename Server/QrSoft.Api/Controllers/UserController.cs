using Application;
using Application.Common.Interfaces;
using Application.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QrSoft.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ApiControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly ICurrentUserService _currentUser;
    public UserController(ILogger<UserController> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }



    [HttpPost]
    [Route("login")]
   public async Task<IActionResult> Login(UserLoginCommand command )
    {
     var result=  await Mediator.Send(command);
        var claims = new Claim[]
        {
            new Claim("Id", result.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(
         claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
         new ClaimsPrincipal(claimsIdentity));
        return Success(result);
    }

    [HttpGet]
    [ClaimRequirement("manager", "customer", "admin")]
    public async Task<IActionResult> GetUserInformation()
    {
        var info = new UserInfoResponse
        {
            GivenName = _currentUser.GivenName,
            Role = _currentUser.Role,
        };
        return Success(info);
    }




}


