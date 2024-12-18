using Looms.PoS.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Exceptions.Handlers;

public class ForbiddenExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception.InnerException is not null)
        {
            exception = exception.InnerException;
        }

        if (exception is not LoomsForbiddenException)
        {
            return false;
        }

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = response.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
