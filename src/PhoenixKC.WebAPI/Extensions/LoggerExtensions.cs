using FluentResults;

namespace PhoenixKC.WebAPI.Extensions;

public static class LoggerExtensions
{
    public static Result LogFailResult<TType>(this ILogger<TType> logger, string messageTemplate, params object?[] args)
    {
        string formatted_message = string.Format(messageTemplate, args);
        logger.LogError(formatted_message);
        return Result.Fail(formatted_message);
    }
    public static Result<TValue> LogFailResult<TType, TValue>(this ILogger<TType> logger, string messageTemplate, params object?[] args)
    {
        string formatted_message = string.Format(messageTemplate, args);
        logger.LogError(formatted_message);
        return Result.Fail<TValue>(formatted_message);
    }
}