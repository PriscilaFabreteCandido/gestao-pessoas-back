using gestao_pessoas_back.Exceptions;
using Newtonsoft.Json;

namespace gestao_pessoas_back.Configurations
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var error = new ExceptionResponse
            {
                Message = exception.Message
            };

            (error.Status) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound),
                BusinessException => (StatusCodes.Status400BadRequest),
                _ => (StatusCodes.Status500InternalServerError)
            };

            context.Response.StatusCode = error.Status;

            var formattedJson = JsonConvert.SerializeObject(error);
            return context.Response.WriteAsync(formattedJson);
        }

    }
}
