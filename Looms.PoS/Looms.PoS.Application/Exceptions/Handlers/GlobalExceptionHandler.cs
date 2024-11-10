using FluentValidation;
using Looms.PoS.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Exceptions.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not LoomsException and ValidationException)
        {
            return false;
        }

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = response.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
