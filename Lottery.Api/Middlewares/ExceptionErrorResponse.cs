using System;

namespace Lottery.Api.Middlewares
{
    public class ExceptionErrorResponse
    {
        public DateTime DateTimeOccurred { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
    }
}
