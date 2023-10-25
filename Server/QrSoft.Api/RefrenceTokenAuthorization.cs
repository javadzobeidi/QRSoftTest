
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
namespace QrSoft.Api;




public class ReferenceTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IMediator _mediator;

    public ReferenceTokenAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IMediator mediator
        )
        : base(options, logger, encoder, clock)
    {
        _mediator = mediator;
    }



    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // skip authentication if endpoint has [AllowAnonymous] attribute
        try
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

           var c= Context.User;


            //  return AuthenticateResult.Fail("Invalid Username or Password");


            ////get user information with mediator
            ///  var result = await _mediator.Send(new GetProfileUserQuery(userId));
            ///  

        

            var claims = new List<Claim>(){
            
        };



            //foreach (var r in userPermissions)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, r));
            //}

            //claims.Add(new Claim(type: "Categories",
            //    value: JsonConvert.SerializeObject(result.groups) ));
            //   return AuthenticateResult.Fail(resultUser.result);

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch(Exception er)
        {
            throw new Exception("اطلاعات ارسالی اشتباه است");
        }
    }
}

