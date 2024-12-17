using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Diagnostics;
using WebApi.src.Domain.Contract;

namespace WebApi.src.Infrastrure.Exception
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                StatusCode = exception is BadHttpRequestException ? (int)
                HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError,
                Title = exception is BadHttpRequestException ? exception.GetType().Name : "An error occured"
            };

            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }
    }
}
