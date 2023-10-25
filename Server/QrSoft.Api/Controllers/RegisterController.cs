using Application;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QrSoft.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class RegisterController : ApiControllerBase
{

    private readonly ILogger<RegisterController> _logger;
  
    public RegisterController(ILogger<RegisterController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("validate/{id}")]
    public async Task<IActionResult> Validate(Guid id,[FromQuery]string inv)
    {
        var result = await Mediator.Send(new ValidateReferallLinkQuery(id,inv));
        return Success(result);
    }

    [HttpPost]
    
    public async Task<IActionResult> Register(UserRegisterCommand command)
    {
        var result = await Mediator.Send(command);
        return Success(result);
    }
}