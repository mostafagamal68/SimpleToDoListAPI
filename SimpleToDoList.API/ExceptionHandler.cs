using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SimpleToDoList.API;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        ProblemDetails problemDetails = exception switch
        {
            KeyNotFoundException => new()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
            },
            _ => new()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
            },
        };

        problemDetails.Detail = exception.InnerException?.Message ?? exception.Message;

        httpContext.Response.StatusCode = problemDetails.Status.GetValueOrDefault();
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
