using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ezzygate.Application.Integrations;
using Ezzygate.WebApi.Dtos;
using Serilog;

namespace Ezzygate.WebApi.Filters;

public abstract class FilterBase : ActionFilterAttribute
{
    protected bool ValidateModel(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return true;
        
        var errors = new StringBuilder();
        foreach (var state in context.ModelState)
        {
            foreach (var error in state.Value.Errors)
            {
                errors.Append(state.Key);
                errors.Append(':');
                errors.Append(error.ErrorMessage);
                errors.Append(error.Exception?.Message);
                errors.Append(", ");
            }
        }

        Log.Error("WebApi app filter validation error: {Errors}", errors.ToString());
            
        // Check if it's an IntegrationController
        var controllerName = context.RouteData.Values["controller"]?.ToString();
        if (controllerName == "Integration")
        {
            context.Result = new ObjectResult(new IntegrationResult { Code = "520", Message = "General error, see logs" })
            {
                StatusCode = 500
            };
        }
        else 
        {
            context.Result = new ObjectResult(new Response(ResultEnum.InvalidRequest, "General error, see logs"))
            {
                StatusCode = 500
            };
        }

        return false;

    }
} 