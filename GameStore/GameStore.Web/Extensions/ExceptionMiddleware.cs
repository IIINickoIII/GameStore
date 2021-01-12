using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using GameStore.Bll.Interfaces;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.Web.Extensions
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly ILoggerService _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode
            };

            switch (exception)
            {
                case InvalidOperationException invalidOperationException:
                case NullReferenceException nullReferenceException:
                case ArgumentNullException argumentNullException:
                case ArgumentException argumentException:
                    errorDetails.Message = exception.Message;
                    break;
                default:
                    errorDetails.Message = "Internal Server Error from the custom middleware.";
                    break;
            }

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}