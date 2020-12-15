using System.Linq;
using Curso.api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Curso.api.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var validaCampoViewModel = new ValidaCampoViewModelOutput(context.ModelState
                .SelectMany(sm => sm.Value.Errors)
                .Select(s => s.ErrorMessage));
            context.Result = new BadRequestObjectResult(validaCampoViewModel);
        }
    }
}