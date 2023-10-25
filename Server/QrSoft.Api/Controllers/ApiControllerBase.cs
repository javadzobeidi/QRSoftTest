
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QrSoft.Api.Filters;
using QrSoft.Api.Models;

namespace QrSoft.Api;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult Success(object result)
    {
        return Ok(new ApiSuccessResult(result));
    }

    protected ActionResult Failure(string message, object data)
    {
        return BadRequest(new ApiErrorResult(message, data));
    }

}
