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
public class ReportController : ApiControllerBase
{

    private readonly ILogger<ReportController> _logger;
    private readonly ICurrentUserService _currentUser;
    public ReportController(ILogger<ReportController> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    [HttpGet]
    [Route("GetUsersCountByType")]
    [ClaimRequirement("admin")]
    public async Task<IActionResult> GetUsersCountByType()
    {
       var result=await Mediator.Send(new GetUsersCountByTypesQuery());
        return Success(new { list = result });
    }

 


}


