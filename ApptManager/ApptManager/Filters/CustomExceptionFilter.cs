using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace ApptManager.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var traceId = context.HttpContext.TraceIdentifier;

            _logger.LogError(exception, "Unhandled exception caught in CustomExceptionFilter. TraceId: {TraceId}", traceId);

            var errorResponse = new
            {
                error = "A handled exception occurred.",
                detail = exception.Message,
                traceId = traceId
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
