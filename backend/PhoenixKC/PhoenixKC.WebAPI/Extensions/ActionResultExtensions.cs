using Microsoft.AspNetCore.Mvc;
using PhoenixKC.WebAPI.Adapters;

namespace PhoenixKC.WebAPI.Extensions;

public static class ActionResultExtensions
{
    public static IResult ToMvcResult(this IActionResult actionResult)
    {
        return new ActionResultAdapter(actionResult);
    }
}