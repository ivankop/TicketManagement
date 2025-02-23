using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketManagement.Model.Exceptions;

namespace TicketManagement.Api
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                EventServiceException => StatusCodes.Status400BadRequest,
                TicketServiceException => StatusCodes.Status400BadRequest,
                UserServiceException => StatusCodes.Status401Unauthorized,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
            if (_env.IsDevelopment())
            {
                context.Result = new ObjectResult(new
                {
                    error = context.Exception.Message,
                    stackTrace = context.Exception.StackTrace
                })
                {
                    StatusCode = statusCode
                };
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    error = context.Exception.Message
                })
                {
                    StatusCode = statusCode
                };

            }

            
            context.ExceptionHandled = true;
        }
    }
}
