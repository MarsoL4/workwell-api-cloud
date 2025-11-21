using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkWell.API.Middleware;

namespace WorkWell.API.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Passa os erros via context.Items para o middleware de erro ler na resposta
                context.HttpContext.Items["ModelStateErrors"] = context.ModelState.GetErrors();
                context.Result = new BadRequestResult(); // Garante status 400
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}