﻿using Looms.PoS.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Exceptions.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not LoomsNotFoundException)
        {
            return false;
        }

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = response.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}