using Lottery.Api.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CustomerCash.Internal.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "An error has occurred");
            }
        }

        /// <summary>
        /// Log complete exception but return customized error message to calling application 
        /// so that code details are not exposed to calling application via exception message
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
        {
            var dateTimeOccured = DateTime.Now;
            _logger.LogError($"DateTimeOccured: {dateTimeOccured} TraceIdentifier: {context.TraceIdentifier} Exception: {exception.Message}");

            context.Response.StatusCode = (int)statusCode;

            var response = new ExceptionErrorResponse
            {
                DateTimeOccurred = dateTimeOccured,
                Message = $"{(int)statusCode} - {message}",
                TraceId = context.TraceIdentifier
            };
            context.Response.ContentType = Application.Json;
            var responseStringContent = JsonSerializer.Serialize(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return context.Response.WriteAsync(responseStringContent);
        }
    }
}