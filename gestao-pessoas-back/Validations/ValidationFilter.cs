using gestao_pessoas_back.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors.Select(err => err.ErrorMessage))
                .ToList();

            var response = new ExceptionResponse
            {
                Status = 400,
                Message = string.Join("; ", errors)
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
