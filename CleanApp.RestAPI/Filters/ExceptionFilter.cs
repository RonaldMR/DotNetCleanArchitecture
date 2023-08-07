using CleanApp.Application.Exceptions;
using CleanApp.RestAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanApp.RestAPI.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var statusCode = 500;

            if(context.Exception is AccessDeniedException)
            {
                statusCode = 401;
            }

            if(context.Exception is AlreadyExistsException)
            {
                statusCode = 400;
            }

            if (context.Exception is EqualToException)
            {
                statusCode = 400;
            }

            if (context.Exception is GreaterThanException)
            {
                statusCode = 400;
            }

            if (context.Exception is NewBookingStatusException)
            {
                statusCode = 400;
            }

            if (context.Exception is NotFoundException)
            {
                statusCode = 404;
            }

            if (context.Exception is NewBookingStatusException)
            {
                statusCode = 400;
            }

            if(context.Exception is RequestErrorException)
            {
                statusCode = 400;
            }

            var message = statusCode == 500 ? "An unexpected error ocurred" : context.Exception.Message;

            var error = new
            {
                StatusCode = statusCode,
                Message = message,
            };

            context.Result = new JsonResult(error) { StatusCode = statusCode };

            return Task.CompletedTask;
        }
    }
}
