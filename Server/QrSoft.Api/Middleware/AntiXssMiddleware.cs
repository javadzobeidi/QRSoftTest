using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using QrSoft.Api.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QrSoft.Api;


public class AntiXssMiddleware
{
    private readonly RequestDelegate _next;
    private ApiErrorResult _error;
    private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

    public AntiXssMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    private static async Task<string> ReadRequestBody(HttpContext context)
    {
        var buffer = new MemoryStream();
        await context.Request.Body.CopyToAsync(buffer);
        context.Request.Body = buffer;
        buffer.Position = 0;

        var encoding = Encoding.UTF8;

        var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
        context.Request.Body.Position = 0;

        return requestContent;
    }
    private async Task RespondWithAnError(HttpContext context,string content)
    {
        context.Response.Clear();
        context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        //context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = _statusCode;

        if (_error == null)
        {
            _error = new ApiErrorResult("Warning: Your input may contain a security issue." , content);            
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(_error));
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Request.Body;
        try
        {
            var content = await ReadRequestBody(context);

            var sanitiser = new HtmlSanitizer();
            var sanitised = sanitiser.Sanitize(content);
            if (content != sanitised.Replace("&amp;", "&"))
            {
                await RespondWithAnError(context, content);
                return;
            }

            await _next(context);
        }
        catch(Exception er)
        {
            await RespondWithAnError(context,"");
        }

    }

}

public static class AntiXssMiddlewareExtension
{
    public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AntiXssMiddleware>();
    }
}

