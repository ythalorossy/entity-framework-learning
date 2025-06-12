using System.Net;
using System.Text.Json;
using Application.Exceptions;

namespace Api.Middleware;

public class GlobalExceptionHandlingMiddleware(
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IHostEnvironment environment)
    : IMiddleware
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ApiErrorResponse();

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException ex:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                response.Errors = ex.Errors;
                break;

            case ApplicationLayerException ex:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                response.Message = ex.Message;
                break;
            
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "An internal server error occurred.";
                if (environment.IsDevelopment())
                    response.DevelopmentDetails = new DevelopmentErrorDetails
                    {
                        Exception = exception.GetType().Name,
                        StackTrace = exception.StackTrace
                    };
                break;
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonSerializerOptions));
    }
}

public class ApiErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; }
    public DevelopmentErrorDetails? DevelopmentDetails { get; set; }
}

public class DevelopmentErrorDetails
{
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
}