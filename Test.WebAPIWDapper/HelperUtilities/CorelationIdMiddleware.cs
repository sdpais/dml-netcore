using System.Diagnostics;
using Microsoft.Extensions.Primitives;
using Serilog.Context;


namespace WebAPIWDapper.HelperUtilities;

public class CorelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string correlationId = "";
        if (!context.Request.Headers.TryGetValue("X-Correlation-ID", out StringValues correlationIds))
        {
            correlationId = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();
        }
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add("X-Correlation-ID", correlationId);
            return Task.CompletedTask;
        });

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }
}
