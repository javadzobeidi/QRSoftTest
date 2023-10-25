using Application;
using Application.Common;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Security.Claims;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly ISender _mediator;

    public CustomCookieAuthenticationEvents(ISender mediator)
    {
        _mediator = mediator;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

       var IdClaim= userPrincipal.Claims.FirstOrDefault(d => d.Type == "Id");
        if (IdClaim == null)
        {
            await Reject(context);
            return;

        }

        var Id = Guid.Parse(IdClaim.Value);
        var userToken = await _mediator.Send(new GetUserByTokenQuery(Id));
        if (userToken == null)
        {
            await Reject(context);
            return;
        }
        var identity = (ClaimsIdentity)context.Principal.Identity;

        /////////////////New Claim
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userToken.User.UserId.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, userToken.User.UserName));
        identity.AddClaim(new Claim(ClaimTypes.GivenName, userToken.User.FirstName+" "+userToken.User.LastName));
        identity.AddClaim(new Claim(ClaimTypes.Role, ((UserTypeEnum)userToken.User.UserTypeId).GetEnumDescription()));


        //if (string.IsNullOrEmpty(lastChanged) ||
        //    !_userRepository.ValidateLastChanged(lastChanged))
        //{
        //    context.RejectPrincipal();

        //    await context.HttpContext.SignOutAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme);
        //}
    }

    private async Task Reject(CookieValidatePrincipalContext context )
    {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(
              CookieAuthenticationDefaults.AuthenticationScheme);
    }
}