using Microsoft.AspNetCore.Diagnostics;
using Sello.Application.DTO;
using Sello.Application.Exceptions;

namespace Sello.Api.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = exception switch
            {
                NotFoundException => exception.Message,
                ForbiddenException => exception.Message,
                BadRequestException => exception.Message,
                _ => "An unexpected error occurred."
            };

            var response = new ErrorResponseDto
            {
                StatusCode = statusCode,
                Message = message
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
