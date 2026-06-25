using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace PhoenixKC.WebAPI.Adapters;

public class ActionResultAdapter(IActionResult thisActionResult) : IResult
{
    #region IResult
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ActionContext action_context = new()
        {
            HttpContext = httpContext,
            RouteData = httpContext.GetRouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        await thisActionResult.ExecuteResultAsync(action_context);
    }
    #endregion
}