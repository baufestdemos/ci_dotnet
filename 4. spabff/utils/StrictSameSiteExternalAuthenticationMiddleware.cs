
using Microsoft.AspNetCore.Authentication;

namespace SpaBff.Utils;

public class StrictSameSiteExternalAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public StrictSameSiteExternalAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext ctx)
    {
        var schemes = ctx.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
        var handlers = ctx.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();

        foreach (var scheme in await schemes.GetRequestHandlerSchemesAsync())
        {
            var handler = await handlers.GetHandlerAsync(ctx, scheme.Name) as IAuthenticationRequestHandler;
            if (handler != null && await handler.HandleRequestAsync())
            {
                string location = default!;
                if (ctx.Response.StatusCode == 302)
                {
                    location = ctx.Response.Headers["location"]!;
                }
                else if (ctx.Request.Method == "GET" && !ctx.Request.Query["skip"].Any())
                {
                    location = ctx.Request.Path + ctx.Request.QueryString + "&skip=1";
                }

                if (location != null)
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.ContentType = "text/html";
                    var html = $@"
                        <html><head>
                            <meta http-equiv='refresh' content='0;url={location}' />
                        </head></html>";
                    await ctx.Response.WriteAsync(html);
                }

                return;
            }
        }

        await _next(ctx);
    }
}